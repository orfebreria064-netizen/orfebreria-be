using AURUM.Modelos.Aplicacion;
using AURUM.Modelos.Ventas;
using Dapper;

namespace AURUM.RepositorioMSSQL;

public class EntregaRepositorio : RepositorioMSSQL
{
    public EntregaRepositorio(string cadenaConexionBD) : base(cadenaConexionBD) { }

    public RespuestaProceso EntregaPrc(Guid idUsuario, ParmEntrega entidad)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@user_id", idUsuario);
        parametros.Add("@id", entidad.id);
        parametros.Add("@order_id", entidad.order_id);
        parametros.Add("@status", entidad.status);
        parametros.Add("@scheduled_date", entidad.scheduled_date);
        parametros.Add("@responsible_id", entidad.responsible_id);
        parametros.Add("@received_by", entidad.received_by);
        parametros.Add("@conformity", entidad.conformity);
        parametros.Add("@conformity_notes", entidad.conformity_notes);
        parametros.Add("@notes", entidad.notes);
        parametros.Add("@branch_id", entidad.branch_id);
        return base.Procesar("api.usp_delivery_save", parametros);
    }

    public RespuestaProceso Firma(Guid idUsuario, Guid idEntrega, string rutaFirma)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@user_id", idUsuario);
        parametros.Add("@id", idEntrega);
        parametros.Add("@signature_path", rutaFirma);
        return base.Procesar("api.usp_delivery_set_signature", parametros);
    }

    public Object Listar(Guid idUsuario, ParmEntregaFiltro filtro)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@user_id", idUsuario);
        parametros.Add("@statuses", Lista(filtro.statuses));
        parametros.Add("@branch_id", filtro.branch_id);
        parametros.Add("@order_id", filtro.order_id);
        parametros.Add("@from", filtro.from);
        parametros.Add("@to", filtro.to);
        return base.Listar("api.usp_deliveries_list", parametros);
    }
}
