using AURUM.Modelos.Aplicacion;
using AURUM.Modelos.Ventas;
using Dapper;

namespace AURUM.RepositorioMSSQL;

public class CotizacionRepositorio : RepositorioMSSQL
{
    public CotizacionRepositorio(string cadenaConexionBD) : base(cadenaConexionBD) { }

    public Object Listar(Guid idUsuario, ParmCotizacionFiltro filtro)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@user_id", idUsuario);
        parametros.Add("@status", filtro.status);
        parametros.Add("@branch_id", filtro.branch_id);
        parametros.Add("@customer_id", filtro.customer_id);
        parametros.Add("@search", filtro.search);
        parametros.Add("@from", filtro.from);
        parametros.Add("@to", filtro.to);
        return base.Listar("api.usp_quotations_list", parametros);
    }

    public Object TraerUno(Guid idUsuario, Guid idCotizacion)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@user_id", idUsuario);
        parametros.Add("@id", idCotizacion);
        return base.ListarMultiple("api.usp_quotation_get", parametros, new[] { "cotizacion", "items" });
    }

    public RespuestaProceso CotizacionPrc(Guid idUsuario, ParmCotizacion entidad)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@user_id", idUsuario);
        parametros.Add("@id", entidad.id);
        parametros.Add("@header", entidad.header.GetRawText());
        parametros.Add("@items", entidad.items.GetRawText());
        return base.Procesar("api.usp_quotation_save", parametros);
    }

    public RespuestaProceso CambiarEstado(Guid idUsuario, ParmCambioEstado entidad)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@user_id", idUsuario);
        parametros.Add("@id", entidad.id);
        parametros.Add("@status", entidad.status);
        return base.Procesar("api.usp_quotation_change_status", parametros);
    }

    public RespuestaProceso Duplicar(Guid idUsuario, Guid idCotizacion)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@user_id", idUsuario);
        parametros.Add("@id", idCotizacion);
        return base.Procesar("api.usp_quotation_duplicate", parametros);
    }

    public RespuestaProceso Vencer(Guid idUsuario)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@user_id", idUsuario);
        return base.Procesar("api.usp_quotations_expire", parametros);
    }

    public RespuestaProceso ConvertirPedido(Guid idUsuario, ParmConvertirPedido entidad)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@user_id", idUsuario);
        parametros.Add("@quotation_id", entidad.quotation_id);
        parametros.Add("@assigned_to", entidad.assigned_to);
        parametros.Add("@due_date", entidad.due_date);
        parametros.Add("@priority", entidad.priority ?? "MEDIA");
        parametros.Add("@create_stages", entidad.create_stages);
        return base.Procesar("api.usp_quotation_convert_to_order", parametros);
    }
}
