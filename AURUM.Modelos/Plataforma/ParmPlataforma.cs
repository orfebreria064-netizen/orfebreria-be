namespace AURUM.Modelos.Plataforma
{
    // Formulario público "Solicitar acceso"
    public class ParmSolicitudAcceso
    {
        public string company_name { get; set; } = "";
        public string? tax_id { get; set; }
        public string contact_name { get; set; } = "";
        public string email { get; set; } = "";
        public string? phone { get; set; }
        public string? plan_code { get; set; }
        public string? message { get; set; }
    }

    public class ParmAprobarSolicitud
    {
        public Guid id { get; set; }
        public string? plan_code { get; set; }
        public int trial_days { get; set; } = 14;
    }

    public class ParmRechazarSolicitud
    {
        public Guid id { get; set; }
        public string? reason { get; set; }
    }

    public class ParmInvitarAdmin
    {
        public Guid org_id { get; set; }
        public string email { get; set; } = "";
        public string? full_name { get; set; }
    }

    public class ParmPagoSuscripcion
    {
        public Guid org_id { get; set; }
        public string plan_code { get; set; } = "";
        public decimal amount { get; set; }
        public string method { get; set; } = "";
        public int months { get; set; } = 1;
        public DateTime? paid_at { get; set; }
        public string? reference { get; set; }
    }

    public class ParmSuscripcion
    {
        public Guid org_id { get; set; }
        public string plan_code { get; set; } = "";
        public string status { get; set; } = "";
        public int extra_users { get; set; }
        public DateTime current_period_end { get; set; }
        public int grace_days { get; set; }
        public string? notes { get; set; }
    }

    public class ParmPlan
    {
        public string code { get; set; } = "";
        public string name { get; set; } = "";
        public int max_users { get; set; }
        public int? max_branches { get; set; }
        public decimal price_month { get; set; }
        public decimal price_year { get; set; }
        public decimal? extra_user_price { get; set; }
        public int position { get; set; }
        public bool active { get; set; } = true;
    }
}
