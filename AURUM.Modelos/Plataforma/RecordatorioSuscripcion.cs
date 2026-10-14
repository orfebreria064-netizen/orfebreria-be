namespace AURUM.Modelos.Plataforma;

// Fila de api.usp_subscription_reminders_due
public class RecordatorioSuscripcion
{
    public Guid org_id { get; set; }
    public string company_name { get; set; } = "";
    public string plan_name { get; set; } = "";
    public string status { get; set; } = "";
    public string state { get; set; } = "";
    public DateTime current_period_end { get; set; }
    public int days_left { get; set; }
    public DateTime grace_end { get; set; }
    public string? admin_emails { get; set; }        // separados por coma
}
