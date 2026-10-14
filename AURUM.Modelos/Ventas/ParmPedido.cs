using System.Text.Json;

namespace AURUM.Modelos.Ventas
{
    public class ParmPedido
    {
        public Guid? id { get; set; }
        public JsonElement header { get; set; }
        public JsonElement items { get; set; }
    }

    public class ParmPedidoFiltro
    {
        public string? status { get; set; }
        public Guid? branch_id { get; set; }
        public Guid? customer_id { get; set; }
        public Guid? assigned_to { get; set; }
        public string? search { get; set; }
        public bool only_active { get; set; }
        public DateTime? from { get; set; }
        public DateTime? to { get; set; }
    }

    public class ParmAsignar
    {
        public Guid id { get; set; }
        public Guid? assigned_to { get; set; }
    }

    public class ParmCronograma
    {
        public Guid id { get; set; }
        public DateTime? due_date { get; set; }
        public string? priority { get; set; }
    }

    public class ParmEtapaAgregar
    {
        public Guid order_id { get; set; }
        public string name { get; set; } = "";
        public int position { get; set; }
        public bool is_quality_check { get; set; }
    }

    public class ParmEtapaActualizar
    {
        public Guid stage_id { get; set; }
        public string status { get; set; } = "";
        public Guid? assigned_to { get; set; }
        public string? notes { get; set; }
    }

    public class ParmProduccionFiltro
    {
        public Guid? branch_id { get; set; }
        public bool only_mine { get; set; }
    }
}
