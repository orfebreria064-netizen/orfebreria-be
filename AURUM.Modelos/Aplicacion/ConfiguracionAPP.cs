namespace AURUM.Modelos.Aplicacion;

public class ConfiguracionAPP
{
    public string BD { get; set; } = "";                  // Cadena de conexión a db_acfa96_orfebreria
    public string UrlFrontend { get; set; } = "";         // Base de los enlaces de los correos (https://app.aurum.pe)
    public string RutaArchivos { get; set; } = "";        // Carpeta donde se guardan los adjuntos de pedidos
    public int ArchivoMaxMB { get; set; } = 10;
    public bool ActivaNotificacion { get; set; }          // false: no envía correos (los registra en el log)
    public bool ActivaRecordatorios { get; set; }         // Job diario de avisos de vencimiento de plan
    public int HoraRecordatorios { get; set; } = 8;       // Hora de Lima en que corre el job
}
