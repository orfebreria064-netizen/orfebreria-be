using AURUM.Modelos.Seguridad;
using AURUM.WebAPI.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AURUM.WebAPI.Services
{
    // Genera el JWT del usuario ya validado por SeguridadAPL.
    // El id del usuario (NameIdentifier) es el @user_id que reciben todos los procedimientos.
    public class AutenticaService : IAutenticaService
    {
        private readonly AppSeguridad _appSeguridad;

        public AutenticaService(IOptions<AppSeguridad> appSeguridad)
        {
            _appSeguridad = appSeguridad.Value;
        }

        public string Autentica(RespuestaLogin usuario)
        {
            var claims = new ClaimsIdentity();
            claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, usuario.user_id!.Value.ToString()));
            claims.AddClaim(new Claim(ClaimTypes.Email, usuario.email ?? ""));
            claims.AddClaim(new Claim(ClaimTypes.Name, usuario.full_name ?? ""));
            if (!string.IsNullOrEmpty(usuario.role)) claims.AddClaim(new Claim(ClaimTypes.Role, usuario.role));
            if (usuario.org_id != null) claims.AddClaim(new Claim("org_id", usuario.org_id.Value.ToString()));
            if (usuario.is_platform_admin) claims.AddClaim(new Claim("platform_admin", "true"));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddHours(_appSeguridad.HorasToken),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSeguridad.Key)), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
        }
    }
}
