using AURUM.Modelos.Reportes;
using Dapper;

namespace AURUM.RepositorioMSSQL;

public class ReporteRepositorio : RepositorioMSSQL
{
    public ReporteRepositorio(string cadenaConexionBD) : base(cadenaConexionBD) { }

    // Dashboard gerencial: JSON con kpis, gráficos y alertas
    public Object? Dashboard(Guid idUsuario, Guid? idSucursal)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@user_id", idUsuario);
        parametros.Add("@branch_id", idSucursal);
        return base.TraerJson("api.usp_dashboard_get", parametros);
    }

    public Object Ventas(Guid idUsuario, ParmReporteVentas filtro)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@user_id", idUsuario);
        parametros.Add("@from", filtro.from ?? InicioMes());
        parametros.Add("@to", filtro.to ?? DateTime.Today);
        parametros.Add("@service_type_id", filtro.service_type_id);
        parametros.Add("@customer_id", filtro.customer_id);
        parametros.Add("@assigned_to", filtro.assigned_to);
        parametros.Add("@branch_id", filtro.branch_id);
        parametros.Add("@currency", filtro.currency);
        return base.Listar("api.usp_report_sales", parametros);
    }

    public Object CuentasPorCobrar(Guid idUsuario, ParmReporteCuentasCobrar filtro)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@user_id", idUsuario);
        parametros.Add("@customer_id", filtro.customer_id);
        parametros.Add("@branch_id", filtro.branch_id);
        parametros.Add("@currency", filtro.currency);
        return base.Listar("api.usp_report_receivables", parametros);
    }

    public Object Cobranza(Guid idUsuario, ParmReporteCobranza filtro)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@user_id", idUsuario);
        parametros.Add("@from", filtro.from ?? InicioMes());
        parametros.Add("@to", filtro.to ?? DateTime.Today);
        parametros.Add("@method", filtro.method);
        parametros.Add("@include_voided", filtro.include_voided);
        parametros.Add("@branch_id", filtro.branch_id);
        parametros.Add("@currency", filtro.currency);
        return base.Listar("api.usp_report_collections", parametros);
    }

    public Object Plazos(Guid idUsuario, ParmReportePlazos filtro)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@user_id", idUsuario);
        parametros.Add("@days_ahead", filtro.days_ahead);
        parametros.Add("@assigned_to", filtro.assigned_to);
        parametros.Add("@branch_id", filtro.branch_id);
        return base.Listar("api.usp_report_order_deadlines", parametros);
    }

    public Object Stock(Guid idUsuario, ParmReporteStock filtro)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@user_id", idUsuario);
        parametros.Add("@type", filtro.type);
        parametros.Add("@include_inactive", filtro.include_inactive);
        parametros.Add("@branch_id", filtro.branch_id);
        return base.Listar("api.usp_report_stock", parametros);
    }

    public Object Custodia(Guid idUsuario, ParmReporteCustodia filtro)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@user_id", idUsuario);
        parametros.Add("@customer_id", filtro.customer_id);
        parametros.Add("@branch_id", filtro.branch_id);
        return base.Listar("api.usp_report_client_custody", parametros);
    }

    // Periodo por defecto de ventas y cobranza: igual que la pantalla (del 1.º del mes a hoy)
    private static DateTime InicioMes() => new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
}
