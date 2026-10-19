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
        public List<string>? statuses { get; set; }
        public List<string>? exclude_statuses { get; set; }
        public Guid? branch_id { get; set; }
        public Guid? customer_id { get; set; }
        public Guid? assigned_to { get; set; }
        public Guid? service_type_id { get; set; }
        public string? search { get; set; }
        public bool only_active { get; set; }
        public bool overdue_only { get; set; }
        public DateTime? from { get; set; }
        public DateTime? to { get; set; }
        public int? limit { get; set; }
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

    // Actualización parcial: lo que viene en null no cambia.
    // update_assigned / update_notes = true para aplicar responsable o notas aunque vengan en null (dejarlos en blanco).
    public class ParmEtapaActualizar
    {
        public Guid stage_id { get; set; }
        public string? status { get; set; }
        public Guid? assigned_to { get; set; }
        public bool update_assigned { get; set; }
        public string? notes { get; set; }
        public bool update_notes { get; set; }
        public string? name { get; set; }
        public int? position { get; set; }
    }

    public class ParmProduccionFiltro
    {
        public Guid? branch_id { get; set; }
        public bool only_mine { get; set; }
    }

    // Orden de producción (taller). Se envían todos los campos: lo que llega en null queda en blanco.
    public class ParmOrdenProduccion
    {
        public Guid order_id { get; set; }
        public Guid? jeweler_id { get; set; }
        public DateTime? material_delivered_at { get; set; }
        public decimal? material_weight_out { get; set; }
        public string? production_instructions { get; set; }
        public DateTime? product_delivered_at { get; set; }
        public decimal? weight_returned { get; set; }
        public string? production_notes { get; set; }
    }

    // doc_type: PRO (proforma) | OP (orden de producción)
    public class ParmEmitirDocumento
    {
        public Guid order_id { get; set; }
        public string doc_type { get; set; } = "";
    }
}
