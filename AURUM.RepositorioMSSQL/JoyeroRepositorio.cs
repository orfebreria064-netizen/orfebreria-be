using AURUM.Modelos.Aplicacion;
using AURUM.Modelos.Produccion;
using Dapper;

namespace AURUM.RepositorioMSSQL;

public class JoyeroRepositorio : RepositorioMSSQL
{
    public JoyeroRepositorio(string cadenaConexionBD) : base(cadenaConexionBD) { }

    public Object Listar(Guid idUsuario, bool incluirInactivos)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@user_id", idUsuario);
        parametros.Add("@include_inactive", incluirInactivos);
        return base.Listar("api.usp_jewelers_list", parametros);
    }

    public RespuestaProceso JoyeroPrc(Guid idUsuario, ParmJoyero entidad)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@user_id", idUsuario);
        parametros.Add("@id", entidad.id);
        parametros.Add("@full_name", entidad.full_name);
        parametros.Add("@document_number", entidad.document_number);
        parametros.Add("@phone", entidad.phone);
        parametros.Add("@specialty", entidad.specialty);
        parametros.Add("@notes", entidad.notes);
        parametros.Add("@active", entidad.active);
        return base.Procesar("api.usp_jeweler_save", parametros);
    }

    public RespuestaProceso RegistrarPago(Guid idUsuario, ParmPagoJoyero entidad)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@user_id", idUsuario);
        parametros.Add("@order_id", entidad.order_id);
        parametros.Add("@jeweler_id", entidad.jeweler_id);
        parametros.Add("@amount", entidad.amount);
        parametros.Add("@method", entidad.method);
        parametros.Add("@concept", entidad.concept);
        parametros.Add("@payment_date", entidad.payment_date);
        parametros.Add("@reference", entidad.reference);
        parametros.Add("@branch_id", entidad.branch_id);
        return base.Procesar("api.usp_jeweler_payment_register", parametros);
    }

    public RespuestaProceso AnularPago(Guid idUsuario, ParmAnularPagoJoyero entidad)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@user_id", idUsuario);
        parametros.Add("@id", entidad.id);
        parametros.Add("@reason", entidad.reason);
        return base.Procesar("api.usp_jeweler_payment_void", parametros);
    }

    public Object ListarPagos(Guid idUsuario, ParmPagoJoyeroFiltro filtro)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@user_id", idUsuario);
        parametros.Add("@jeweler_id", filtro.jeweler_id);
        parametros.Add("@order_id", filtro.order_id);
        parametros.Add("@branch_id", filtro.branch_id);
        parametros.Add("@from", filtro.from);
        parametros.Add("@to", filtro.to);
        parametros.Add("@include_voided", filtro.include_voided);
        return base.Listar("api.usp_jeweler_payments_list", parametros);
    }
}
