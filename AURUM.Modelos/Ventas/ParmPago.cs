namespace AURUM.Modelos.Ventas
{
    public class ParmPago
    {
        public Guid order_id { get; set; }
        public decimal amount { get; set; }
        public string method { get; set; } = "";
        public DateTime? payment_date { get; set; }
        public string? reference { get; set; }
        public string? notes { get; set; }
        public bool is_advance { get; set; }
        public bool overpayment_confirmed { get; set; }
        public Guid? branch_id { get; set; }               // null = sucursal del pedido
        public string? received_from { get; set; }         // quien entrega el dinero, si no es el cliente
    }

    public class ParmAnularPago
    {
        public Guid payment_id { get; set; }
        public string reason { get; set; } = "";
    }

    public class ParmPagoFiltro
    {
        public Guid? order_id { get; set; }
        public Guid? customer_id { get; set; }
        public Guid? branch_id { get; set; }
        public string? method { get; set; }
        public DateTime? from { get; set; }
        public DateTime? to { get; set; }
        public bool include_voided { get; set; } = true;
    }

    public class ParmEntrega
    {
        public Guid? id { get; set; }
        public Guid? order_id { get; set; }
        public string status { get; set; } = "PENDIENTE";
        public DateTime? scheduled_date { get; set; }
        public Guid? responsible_id { get; set; }
        public string? received_by { get; set; }
        public bool? conformity { get; set; }
        public string? conformity_notes { get; set; }
        public string? notes { get; set; }
        public Guid? branch_id { get; set; }
    }

    public class ParmEntregaFiltro
    {
        public List<string>? statuses { get; set; }
        public Guid? branch_id { get; set; }
        public Guid? order_id { get; set; }
        public DateTime? from { get; set; }
        public DateTime? to { get; set; }
    }
}
