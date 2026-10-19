using AURUM.Modelos.Aplicacion;
using AURUM.WebAPI.Models;
using AURUM.WebAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Text;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

// Configurar Serilog para consola y archivo
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .WriteTo.File("logs/app-log.txt", rollingInterval: RollingInterval.Day)
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddControllers()
    .AddJsonOptions(o => o.JsonSerializerOptions.Converters.Add(new FechaJsonConverter()));
builder.Services.AddEndpointsApiExplorer();

//Configuration
builder.Services.Configure<ConfiguracionAPP>(builder.Configuration.GetSection("ConfiguracionApp"));
builder.Services.Configure<AppSeguridad>(builder.Configuration.GetSection("SeguridadAPP"));

builder.Services.AddScoped<IAutenticaService, AutenticaService>();
builder.Services.AddHostedService<RecordatorioJob>();

//JWT
var seguridadApp = builder.Configuration.GetSection("SeguridadAPP").Get<AppSeguridad>() ?? new AppSeguridad();
if (seguridadApp.Key.Length < 32)
    throw new InvalidOperationException("SeguridadAPP:Key debe tener al menos 32 caracteres.");
var keyBytes = Encoding.UTF8.GetBytes(seguridadApp.Key);
builder.Services.AddAuthentication(config =>
{
    config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(config =>
{
    config.RequireHttpsMetadata = false;
    config.SaveToken = true;
    config.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.FromMinutes(1)
    };
});

// Límite para endpoints públicos (login, invitaciones, recuperación, solicitud de acceso):
// 20 por minuto por IP y endpoint. El login además bloquea la cuenta tras 5 fallos (en la base).
builder.Services.AddRateLimiter(options =>
{
    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
    options.AddPolicy("publico", context => RateLimitPartition.GetFixedWindowLimiter(
        $"{context.Connection.RemoteIpAddress}:{context.Request.Path}",
        _ => new FixedWindowRateLimiterOptions { PermitLimit = 20, Window = TimeSpan.FromMinutes(1) }));
});

// CORS: orígenes del frontend en ConfiguracionApp:OrigenesPermitidos (vacío = cualquiera, solo desarrollo)
var origenes = builder.Configuration.GetSection("ConfiguracionApp:OrigenesPermitidos").Get<string[]>() ?? Array.Empty<string>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("Frontend", politica =>
    {
        if (origenes.Length > 0) politica.WithOrigins(origenes);
        else politica.AllowAnyOrigin();
        politica.AllowAnyMethod().AllowAnyHeader();
    });
});

//SWAGGER GEN
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "AURUM API - Orfebrería", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header usando el Bearer scheme. \r\n\r\n Ingrese 'Bearer' [space] y luego el token.\r\n\r\nEjemplo: \"Bearer 1safsfsdfdfd\"",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme { Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" } },
            new string[] {}
        }
    });
});

var app = builder.Build();

app.UseCors("Frontend");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// En desarrollo el frontend llama por http://localhost:5227: redirigir a https rompe las llamadas del navegador
if (!app.Environment.IsDevelopment())
    app.UseHttpsRedirection();
app.UseRateLimiter();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
