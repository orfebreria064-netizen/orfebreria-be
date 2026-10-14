using System.Net;

namespace AURUM.Notifica
{
    // Cuerpos HTML de los correos del sistema
    public static class PlantillaCorreo
    {
        static string Html(string titulo, string cuerpo) => $@"
<div style=""font-family:Segoe UI,Arial,sans-serif;max-width:560px;margin:auto;color:#2b2b2b"">
  <h2 style=""color:#8a6d1d;margin-bottom:8px"">{WebUtility.HtmlEncode(titulo)}</h2>
  {cuerpo}
  <p style=""color:#888;font-size:12px;margin-top:32px"">Orfebrería Aurum · Este es un mensaje automático, no responda a este correo.</p>
</div>";

        static string Boton(string texto, string enlace) =>
            $@"<p style=""margin:24px 0""><a href=""{WebUtility.HtmlEncode(enlace)}"" style=""background:#8a6d1d;color:#fff;padding:12px 20px;border-radius:6px;text-decoration:none"">{WebUtility.HtmlEncode(texto)}</a></p>";

        static string RolLegible(string rol) => rol switch
        {
            "ADMIN" => "Administrador",
            "GERENTE" => "Gerente",
            "VENDEDOR" => "Vendedor",
            "SUPERVISOR" => "Supervisor de taller",
            "ORFEBRE" => "Orfebre",
            _ => rol
        };

        public static string Invitacion(string? nombre, string empresa, string rol, string enlace) => Html(
            $"Le invitaron a {empresa}",
            $@"<p>Hola{(string.IsNullOrWhiteSpace(nombre) ? "" : " " + WebUtility.HtmlEncode(nombre))},</p>
<p>Fue invitado a usar el sistema de <b>{WebUtility.HtmlEncode(empresa)}</b> con el rol <b>{RolLegible(rol)}</b>.
Para activar su cuenta, cree su contraseña:</p>
{Boton("Crear mi contraseña", enlace)}
<p>El enlace vence en 7 días.</p>");

        public static string RecuperarClave(string? nombre, string enlace) => Html(
            "Restablecer contraseña",
            $@"<p>Hola{(string.IsNullOrWhiteSpace(nombre) ? "" : " " + WebUtility.HtmlEncode(nombre))},</p>
<p>Recibimos una solicitud para restablecer su contraseña.</p>
{Boton("Crear una nueva contraseña", enlace)}
<p>El enlace vence en 1 hora. Si no lo solicitó, ignore este correo.</p>");

        public static string RecordatorioPlan(string empresa, string plan, string estado, int diasRestantes, DateTime vencimiento, DateTime finGracia)
        {
            string detalle = estado switch
            {
                "GRACE" => $"El plan venció el {vencimiento:dd/MM/yyyy}. Tiene hasta el {finGracia:dd/MM/yyyy} para regularizar el pago antes de que el sistema pase a modo solo lectura.",
                "EXPIRED" => "El plan está vencido: el sistema está en modo solo lectura hasta que se registre el pago.",
                _ when diasRestantes == 0 => $"El plan {plan} vence hoy.",
                _ => $"El plan {plan} vence en {diasRestantes} día(s), el {vencimiento:dd/MM/yyyy}."
            };
            return Html($"Plan de {empresa}", $"<p>{WebUtility.HtmlEncode(detalle)}</p><p>Para renovar, comuníquese con nosotros.</p>");
        }
    }
}
