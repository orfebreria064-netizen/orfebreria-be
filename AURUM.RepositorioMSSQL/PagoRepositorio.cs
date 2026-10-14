using AURUM.Modelos.Aplicacion;
using AURUM.Modelos.Ventas;
using Dapper;

namespace AURUM.RepositorioMSSQL;

public class PagoRepositorio : RepositorioMSSQL
{
    public PagoRepositorio(string cadenaConexionBD) : base(cadenaConexionBD) { }

    public RespuestaProceso Registrar(Guid idUsuario, ParmPago entidad)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@user_id", idUsuario);
        parametros.Add("@order_id", entidad.order_id);
        parametros.Add("@amount", entidad.amount);
        parametros.Add("@method", entidad.method);
        parametros.Add("@payment_date", entidad.payment_date);
        parametros.Add("@reference", entidad.reference);
        parametros.Add("@notes", entidad.notes);
        parametros.Add("@is_advance", entidad.is_advance);
        parametros.Add("@overpayment_confirmed", entidad.overpayment_confirmed);
        parametros.Add("@branch_id", entidad.branch_id);
        return base.Procesar("api.usp_payment_register", parametros);
    }

    public RespuestaProceso Anular(Guid idUsuario, ParmAnularPago entidad)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@user_id", idUsuario);
        parametros.Add("@payment_id", entidad.payment_id);
        parametros.Add("@reason", entidad.reason);
        return base.Procesar("api.usp_payment_void", parametros);
    }

    public Object Listar(Guid idUsuario, ParmPagoFiltro filtro)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@user_id", idUsuario);
        parametros.Add("@order_id", filtro.order_id);
        parametros.Add("@branch_id", filtro.branch_id);
        parametros.Add("@from", filtro.from);
        parametros.Add("@to", filtro.to);
        parametros.Add("@include_voided", filtro.include_voided);
        return base.Listar("api.usp_payments_list", parametros);
    }

    public Object PorCobrar(Guid idUsuario, Guid? idSucursal)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@user_id", idUsuario);
        parametros.Add("@branch_id", idSucursal);
        return base.Listar("api.usp_receivables_list", parametros);
    }
}
