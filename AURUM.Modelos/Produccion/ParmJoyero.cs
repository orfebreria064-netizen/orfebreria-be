namespace AURUM.Modelos.Produccion
{
    // Joyero externo (no usa el sistema)
    public class ParmJoyero
    {
        public Guid? id { get; set; }
        public string full_name { get; set; } = "";
        public string? document_number { get; set; }       // DNI
        public string? phone { get; set; }
        public string? specialty { get; set; }
        public string? notes { get; set; }
        public bool active { get; set; } = true;
    }

    // Recibo de egreso: pago a un joyero por un pedido (la moneda es la del pedido)
    public class ParmPagoJoyero
    {
        public Guid order_id { get; set; }
        public Guid jeweler_id { get; set; }
        public decimal amount { get; set; }
        public string method { get; set; } = "EFECTIVO";
        public string concept { get; set; } = "";
        public DateTime? payment_date { get; set; }
        public string? reference { get; set; }
        public Guid? branch_id { get; set; }               // null = sucursal del pedido
    }

    public class ParmAnularPagoJoyero
    {
        public Guid id { get; set; }
        public string reason { get; set; } = "";
    }

    public class ParmPagoJoyeroFiltro
    {
        public Guid? jeweler_id { get; set; }
        public Guid? order_id { get; set; }
        public Guid? branch_id { get; set; }
        public DateTime? from { get; set; }
        public DateTime? to { get; set; }
        public bool include_voided { get; set; }
    }
}
