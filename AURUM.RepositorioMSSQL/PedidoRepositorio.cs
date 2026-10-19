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
        parametros.Add("@statuses", Lista(filtro.statuses));
        parametros.Add("@exclude_statuses", Lista(filtro.exclude_statuses));
        parametros.Add("@branch_id", filtro.branch_id);
        parametros.Add("@customer_id", filtro.customer_id);
        parametros.Add("@assigned_to", filtro.assigned_to);
        parametros.Add("@service_type_id", filtro.service_type_id);
        parametros.Add("@search", filtro.search);
        parametros.Add("@only_active", filtro.only_active);
        parametros.Add("@overdue_only", filtro.overdue_only);
        parametros.Add("@from", filtro.from);
        parametros.Add("@to", filtro.to);
        parametros.Add("@limit", filtro.limit);
        return base.Listar("api.usp_orders_list", parametros);
    }

    // Pedido con ítems, etapas, historial, pagos, entregas, adjuntos, material del cliente y pagos a joyeros
    public Object TraerUno(Guid idUsuario, Guid idPedido)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@user_id", idUsuario);
        parametros.Add("@id", idPedido);
        return base.ListarMultiple("api.usp_order_get", parametros,
            new[] { "pedido", "items", "etapas", "historial", "pagos", "entregas", "adjuntos", "material_cliente", "pagos_joyero" });
    }

    // Orden de producción: joyero, material entregado, indicaciones y acabado
    public RespuestaProceso OrdenProduccion(Guid idUsuario, ParmOrdenProduccion entidad)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@user_id", idUsuario);
        parametros.Add("@order_id", entidad.order_id);
        parametros.Add("@jeweler_id", entidad.jeweler_id);
        parametros.Add("@material_delivered_at", entidad.material_delivered_at);
        parametros.Add("@material_weight_out", entidad.material_weight_out);
        parametros.Add("@production_instructions", entidad.production_instructions);
        parametros.Add("@product_delivered_at", entidad.product_delivered_at);
        parametros.Add("@weight_returned", entidad.weight_returned);
        parametros.Add("@production_notes", entidad.production_notes);
        return base.Procesar("api.usp_order_production_save", parametros);
    }

    // Proforma (PRO) u orden de producción (OP): asigna el número la primera vez
    public RespuestaProceso EmitirDocumento(Guid idUsuario, ParmEmitirDocumento entidad)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@user_id", idUsuario);
        parametros.Add("@order_id", entidad.order_id);
        parametros.Add("@doc_type", entidad.doc_type);
        return base.Procesar("api.usp_order_issue_document", parametros);
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
        parametros.Add("@set_assigned", entidad.update_assigned);
        parametros.Add("@notes", entidad.notes);
        parametros.Add("@set_notes", entidad.update_notes);
        parametros.Add("@name", entidad.name);
        parametros.Add("@position", entidad.position);
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
