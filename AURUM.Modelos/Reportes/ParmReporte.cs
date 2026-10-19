namespace AURUM.Modelos.Reportes
{
    public class ParmReporteVentas
    {
        public DateTime? from { get; set; }               // null = 1.º del mes actual
        public DateTime? to { get; set; }                 // null = hoy
        public Guid? service_type_id { get; set; }
        public Guid? customer_id { get; set; }
        public Guid? assigned_to { get; set; }
        public Guid? branch_id { get; set; }
        public string? currency { get; set; }              // PEN | USD (null = todas)
    }

    public class ParmReporteCuentasCobrar
    {
        public Guid? customer_id { get; set; }
        public Guid? branch_id { get; set; }
        public string? currency { get; set; }              // PEN | USD (null = todas)
    }

    public class ParmReporteCobranza
    {
        public DateTime? from { get; set; }               // null = 1.º del mes actual
        public DateTime? to { get; set; }                 // null = hoy
        public string? method { get; set; }
        public bool include_voided { get; set; }
        public Guid? branch_id { get; set; }
        public string? currency { get; set; }              // PEN | USD (null = todas)
    }

    public class ParmReportePlazos
    {
        public int days_ahead { get; set; } = 7;
        public Guid? assigned_to { get; set; }
        public Guid? branch_id { get; set; }
    }

    public class ParmReporteStock
    {
        public string? type { get; set; }
        public bool include_inactive { get; set; }
        public Guid? branch_id { get; set; }
    }

    public class ParmReporteCustodia
    {
        public Guid? customer_id { get; set; }
        public Guid? branch_id { get; set; }
    }
}
