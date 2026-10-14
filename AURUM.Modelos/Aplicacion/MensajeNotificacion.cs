namespace AURUM.Modelos.Aplicacion;

public class MensajeNotificacion
{
    public string emailDestino { get; set; } = "";       // Uno o varios, separados por coma
    public string asunto { get; set; } = "";
    public string html_email_body { get; set; } = "";
}
