namespace AURUM.Modelos.Aplicacion;

// Cambio de estado de cotizaciones y pedidos
public class ParmCambioEstado
{
    public Guid id { get; set; }
    public string status { get; set; } = "";
}
