using AURUM.Modelos.Aplicacion;
using System.Net;
using System.Net.Mail;

namespace AURUM.Notifica
{
    public class CorreoAPL
    {
        readonly NotificacionSmtp _smtp;
        readonly bool _activo;

        public CorreoAPL(NotificacionSmtp smtp, bool activo)
        {
            _smtp = smtp;
            _activo = activo;
        }

        // Devuelve false si no se pudo enviar. Con la notificación desactivada solo lo registra en consola.
        public bool enviarCorreo(MensajeNotificacion parmMensaje)
        {
            if (!_activo || string.IsNullOrWhiteSpace(_smtp.Host))
            {
                Console.WriteLine($"[CorreoAPL] Notificación desactivada. Para: {parmMensaje.emailDestino} - {parmMensaje.asunto}");
                return false;
            }

            using var mailMensaje = new MailMessage
            {
                From = new MailAddress(_smtp.Usuario, _smtp.Remitente),
                Subject = parmMensaje.asunto,
                Body = parmMensaje.html_email_body,
                IsBodyHtml = true
            };
            foreach (var destino in parmMensaje.emailDestino.Split(new[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries))
                mailMensaje.To.Add(destino);

            using var smtpClient = new SmtpClient(_smtp.Host, _smtp.Puerto)
            {
                EnableSsl = _smtp.Ssl,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(_smtp.Usuario, _smtp.Password)
            };
            try
            {
                smtpClient.Send(mailMensaje);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[CorreoAPL] Error al enviar a {parmMensaje.emailDestino}: {ex.Message}");
                return false;
            }
        }
    }
}
