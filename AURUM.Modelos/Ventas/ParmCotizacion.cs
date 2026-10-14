using System.Text.Json;

namespace AURUM.Modelos.Ventas
{
    // Cabecera e ítems viajan como JSON, igual que en las RPC save_quotation / save_order.
    public class ParmCotizacion
    {
        public Guid? id { get; set; }
        public JsonElement header { get; set; }
        public JsonElement items { get; set; }
    }

    public class ParmCotizacionFiltro
    {
        public string? status { get; set; }
        public Guid? branch_id { get; set; }
        public Guid? customer_id { get; set; }
        public string? search { get; set; }
        public DateTime? from { get; set; }
        public DateTime? to { get; set; }
    }

    public class ParmConvertirPedido
    {
        public Guid quotation_id { get; set; }
        public Guid? assigned_to { get; set; }
        public DateTime? due_date { get; set; }
        public string? priority { get; set; }
        public bool create_stages { get; set; } = true;
    }
}
