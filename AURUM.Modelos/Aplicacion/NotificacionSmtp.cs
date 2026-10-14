namespace AURUM.Modelos.Aplicacion;

public class NotificacionSmtp
{
    public string Host { get; set; } = "";
    public int Puerto { get; set; } = 587;
    public bool Ssl { get; set; } = true;
    public string Usuario { get; set; } = "";
    public string Password { get; set; } = "";
    public string Remitente { get; set; } = "Orfebrería Aurum";
}
