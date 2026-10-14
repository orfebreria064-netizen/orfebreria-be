using AURUM.Modelos.Aplicacion;
using AURUM.Modelos.Ventas;
using Dapper;

namespace AURUM.RepositorioMSSQL;

public class PedidoRepositorio : RepositorioMSSQL
{
    public PedidoRepositorio(string cadenaConexionBD) : base(cadenaConexionBD) { }

    public Object Listar(Guid idUsuario, ParmPedidoFiltro filtro)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@user_id", idUsuario);
        parametros.Add("@status", filtro.status);
        parametros.Add("@branch_id", filtro.branch_id);
        parametros.Add("@customer_id", filtro.customer_id);
        parametros.Add("@assigned_to", filtro.assigned_to);
        parametros.Add("@search", filtro.search);
        parametros.Add("@only_active", filtro.only_active);
        parametros.Add("@from", filtro.from);
        parametros.Add("@to", filtro.to);
        return base.Listar("api.usp_orders_list", parametros);
    }

    // Pedido con ítems, etapas, historial, pagos, entregas, adjuntos y material del cliente
    public Object TraerUno(Guid idUsuario, Guid idPedido)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@user_id", idUsuario);
        parametros.Add("@id", idPedido);
        return base.ListarMultiple("api.usp_order_get", parametros,
            new[] { "pedido", "items", "etapas", "historial", "pagos", "entregas", "adjuntos", "material_cliente" });
    }

    public RespuestaProceso PedidoPrc(Guid idUsuario, ParmPedido entidad)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@user_id", idUsuario);
        parametros.Add("@id", entidad.id);
        parametros.Add("@header", entidad.header.GetRawText());
        parametros.Add("@items", entidad.items.GetRawText());
        return base.Procesar("api.usp_order_save", parametros);
    }

    public RespuestaProceso CambiarEstado(Guid idUsuario, ParmCambioEstado entidad)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@user_id", idUsuario);
        parametros.Add("@id", entidad.id);
        parametros.Add("@status", entidad.status);
        return base.Procesar("api.usp_order_change_status", parametros);
    }

    public RespuestaProceso Asignar(Guid idUsuario, ParmAsignar entidad)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@user_id", idUsuario);
        parametros.Add("@id", entidad.id);
        parametros.Add("@assigned_to", entidad.assigned_to);
        return base.Procesar("api.usp_order_assign", parametros);
    }

    public RespuestaProceso Cronograma(Guid idUsuario, ParmCronograma entidad)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@user_id", idUsuario);
        parametros.Add("@id", entidad.id);
        parametros.Add("@due_date", entidad.due_date);
        parametros.Add("@priority", entidad.priority);
        return base.Procesar("api.usp_order_update_schedule", parametros);
    }

    public RespuestaProceso GenerarEtapas(Guid idUsuario, Guid idPedido)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@user_id", idUsuario);
        parametros.Add("@order_id", idPedido);
        return base.Procesar("api.usp_order_create_stages", parametros);
    }

    public RespuestaProceso AgregarEtapa(Guid idUsuario, ParmEtapaAgregar entidad)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@user_id", idUsuario);
        parametros.Add("@order_id", entidad.order_id);
        parametros.Add("@name", entidad.name);
        parametros.Add("@position", entidad.position);
        parametros.Add("@is_quality_check", entidad.is_quality_check);
        return base.Procesar("api.usp_order_stage_add", parametros);
    }

    public RespuestaProceso ActualizarEtapa(Guid idUsuario, ParmEtapaActualizar entidad)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@user_id", idUsuario);
        parametros.Add("@stage_id", entidad.stage_id);
        parametros.Add("@status", entidad.status);
        parametros.Add("@assigned_to", entidad.assigned_to);
        parametros.Add("@notes", entidad.notes);
        return base.Procesar("api.usp_order_stage_update", parametros);
    }

    public RespuestaProceso EliminarEtapa(Guid idUsuario, Guid idEtapa)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@user_id", idUsuario);
        parametros.Add("@stage_id", idEtapa);
        return base.Procesar("api.usp_order_stage_delete", parametros);
    }

    public Object Produccion(Guid idUsuario, ParmProduccionFiltro filtro)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@user_id", idUsuario);
        parametros.Add("@branch_id", filtro.branch_id);
        parametros.Add("@only_mine", filtro.only_mine);
        return base.Listar("api.usp_production_list", parametros);
    }
}
