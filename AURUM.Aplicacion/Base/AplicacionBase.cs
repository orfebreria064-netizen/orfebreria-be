using AURUM.Modelos.Aplicacion;
using AURUM.Notifica;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace AURUM.Aplicacion.Base
{
    public class AplicacionBase
    {
        public IConfiguration configuracion;
        public ConfiguracionAPP parametrosApp;

        public AplicacionBase(IConfiguration iConfiguration, IOptions<ConfiguracionAPP> iConfiguracionAPP)
        {
            configuracion = iConfiguration;
            parametrosApp = iConfiguracionAPP.Value;
        }

        protected CorreoAPL Correo()
        {
            var smtp = configuracion.GetSection("NotificacionSmtp").Get<NotificacionSmtp>() ?? new NotificacionSmtp();
            return new CorreoAPL(smtp, parametrosApp.ActivaNotificacion);
        }

        protected string EnlaceFrontend(string ruta) => $"{parametrosApp.UrlFrontend.TrimEnd('/')}/{ruta.TrimStart('/')}";

        // Copia la fila devuelta por el procedimiento sin las columnas sensibles (p. ej. el token del correo)
        protected static Dictionary<string, object?>? SinColumnas(object? fila, params string[] columnas)
        {
            if (fila is not IDictionary<string, object?> datos) return null;
            return datos.Where(c => !columnas.Contains(c.Key)).ToDictionary(c => c.Key, c => c.Value);
        }

        protected static string? Valor(object? fila, string columna) =>
            fila is IDictionary<string, object?> datos && datos.TryGetValue(columna, out var valor) ? valor?.ToString() : null;
    }
}
