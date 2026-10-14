using AURUM.Modelos.Aplicacion;
using AURUM.Modelos.Plataforma;
using Dapper;

namespace AURUM.RepositorioMSSQL;

public class PlataformaRepositorio : RepositorioMSSQL
{
    public PlataformaRepositorio(string cadenaConexionBD) : base(cadenaConexionBD) { }

    // ---------- Públicos ----------
    public Object ListarPlanes(bool incluirInactivos)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@include_inactive", incluirInactivos);
        return base.Listar("api.usp_plans_list", parametros);
    }

    public RespuestaProceso SolicitarAcceso(ParmSolicitudAcceso entidad)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@company_name", entidad.company_name);
        parametros.Add("@tax_id", entidad.tax_id);
        parametros.Add("@contact_name", entidad.contact_name);
        parametros.Add("@email", entidad.email);
        parametros.Add("@phone", entidad.phone);
        parametros.Add("@plan_code", entidad.plan_code);
        parametros.Add("@message", entidad.message);
        return base.Procesar("api.usp_access_request_submit", parametros);
    }

    // ---------- Superadministrador ----------
    public RespuestaProceso ActualizarPlan(Guid idUsuario, ParmPlan entidad)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@user_id", idUsuario);
        parametros.Add("@code", entidad.code);
        parametros.Add("@name", entidad.name);
        parametros.Add("@max_users", entidad.max_users);
        parametros.Add("@max_branches", entidad.max_branches);
        parametros.Add("@price_month", entidad.price_month);
        parametros.Add("@price_year", entidad.price_year);
        parametros.Add("@extra_user_price", entidad.extra_user_price);
        parametros.Add("@position", entidad.position);
        parametros.Add("@active", entidad.active);
        return base.Procesar("api.usp_plan_update", parametros);
    }

    public Object ListarSolicitudes(Guid idUsuario, string? estado)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@user_id", idUsuario);
        parametros.Add("@status", estado);
        return base.Listar("api.usp_platform_access_requests", parametros);
    }

    public RespuestaProceso AprobarSolicitud(Guid idUsuario, ParmAprobarSolicitud entidad)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@user_id", idUsuario);
        parametros.Add("@id", entidad.id);
        parametros.Add("@plan_code", entidad.plan_code);
        parametros.Add("@trial_days", entidad.trial_days);
        return base.Procesar("api.usp_platform_approve_request", parametros);
    }

    public RespuestaProceso RechazarSolicitud(Guid idUsuario, ParmRechazarSolicitud entidad)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@user_id", idUsuario);
        parametros.Add("@id", entidad.id);
        parametros.Add("@reason", entidad.reason);
        return base.Procesar("api.usp_platform_reject_request", parametros);
    }

    public RespuestaProceso InvitarAdmin(Guid idUsuario, ParmInvitarAdmin entidad)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@user_id", idUsuario);
        parametros.Add("@org_id", entidad.org_id);
        parametros.Add("@email", entidad.email);
        parametros.Add("@full_name", entidad.full_name);
        return base.Procesar("api.usp_platform_invite_admin", parametros);
    }

    public RespuestaProceso RegistrarPago(Guid idUsuario, ParmPagoSuscripcion entidad)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@user_id", idUsuario);
        parametros.Add("@org_id", entidad.org_id);
        parametros.Add("@plan_code", entidad.plan_code);
        parametros.Add("@amount", entidad.amount);
        parametros.Add("@method", entidad.method);
        parametros.Add("@months", entidad.months);
        parametros.Add("@paid_at", entidad.paid_at);
        parametros.Add("@reference", entidad.reference);
        return base.Procesar("api.usp_platform_register_payment", parametros);
    }

    public RespuestaProceso ActualizarSuscripcion(Guid idUsuario, ParmSuscripcion entidad)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@user_id", idUsuario);
        parametros.Add("@org_id", entidad.org_id);
        parametros.Add("@plan_code", entidad.plan_code);
        parametros.Add("@status", entidad.status);
        parametros.Add("@extra_users", entidad.extra_users);
        parametros.Add("@current_period_end", entidad.current_period_end);
        parametros.Add("@grace_days", entidad.grace_days);
        parametros.Add("@notes", entidad.notes);
        return base.Procesar("api.usp_platform_update_subscription", parametros);
    }

    public Object ListarEmpresas(Guid idUsuario)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@user_id", idUsuario);
        return base.Listar("api.usp_platform_organizations", parametros);
    }

    public Object? Resumen(Guid idUsuario)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@user_id", idUsuario);
        return base.TraerJson("api.usp_platform_overview", parametros);
    }

    // Superadmin: todos o de una empresa. ADMIN de empresa: los suyos.
    public Object PagosSuscripcion(Guid idUsuario, Guid? idEmpresa)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@user_id", idUsuario);
        parametros.Add("@org_id", idEmpresa);
        return base.Listar("api.usp_subscription_payments_list", parametros);
    }

    // Job de recordatorios (sin usuario)
    public List<RecordatorioSuscripcion> RecordatoriosPendientes()
    {
        return base.Listar<RecordatorioSuscripcion>("api.usp_subscription_reminders_due", new DynamicParameters());
    }
}
