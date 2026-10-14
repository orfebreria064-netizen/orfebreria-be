namespace AURUM.Modelos.Reportes
{
    public class ParmReporteVentas
    {
        public DateTime from { get; set; }
        public DateTime to { get; set; }
        public Guid? service_type_id { get; set; }
        public Guid? customer_id { get; set; }
        public Guid? assigned_to { get; set; }
        public Guid? branch_id { get; set; }
    }

    public class ParmReporteCuentasCobrar
    {
        public Guid? customer_id { get; set; }
        public Guid? branch_id { get; set; }
    }

    public class ParmReporteCobranza
    {
        public DateTime from { get; set; }
        public DateTime to { get; set; }
        public string? method { get; set; }
        public bool include_voided { get; set; }
        public Guid? branch_id { get; set; }
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
