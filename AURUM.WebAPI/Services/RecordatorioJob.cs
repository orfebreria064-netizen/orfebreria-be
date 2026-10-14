using AURUM.Aplicacion.Plataforma;
using AURUM.Modelos.Aplicacion;
using Microsoft.Extensions.Options;

namespace AURUM.WebAPI.Services
{
    // Una vez al día, a la HoraRecordatorios de Lima, envía los avisos de vencimiento de plan
    // (7, 3, 1 y 0 días antes, al entrar en gracia y al vencer).
    public class RecordatorioJob : BackgroundService
    {
        private readonly IConfiguration _configuration;
        private readonly IOptions<ConfiguracionAPP> _configuracionAPP;
        private readonly ILogger<RecordatorioJob> _logger;
        private static readonly TimeZoneInfo Lima = TimeZoneInfo.FindSystemTimeZoneById(OperatingSystem.IsWindows() ? "SA Pacific Standard Time" : "America/Lima");
        private DateOnly? _ultimaEjecucion;

        public RecordatorioJob(IConfiguration configuration, IOptions<ConfiguracionAPP> configuracionAPP, ILogger<RecordatorioJob> logger)
        {
            _configuration = configuration;
            _configuracionAPP = configuracionAPP;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (!_configuracionAPP.Value.ActivaRecordatorios) return;

            while (!stoppingToken.IsCancellationRequested)
            {
                var ahora = TimeZoneInfo.ConvertTime(DateTimeOffset.UtcNow, Lima);
                var hoy = DateOnly.FromDateTime(ahora.DateTime);
                if (ahora.Hour >= _configuracionAPP.Value.HoraRecordatorios && _ultimaEjecucion != hoy)
                {
                    try
                    {
                        int enviados = new PlataformaAPL(_configuration, _configuracionAPP).EnviarRecordatorios();
                        _ultimaEjecucion = hoy;
                        _logger.LogInformation("Recordatorios de plan enviados: {Enviados}", enviados);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error al enviar recordatorios de plan");
                    }
                }
                await Task.Delay(TimeSpan.FromMinutes(15), stoppingToken);
            }
        }
    }
}
