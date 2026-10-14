using AURUM.Modelos.Aplicacion;
using AURUM.Modelos.Materiales;
using Dapper;

namespace AURUM.RepositorioMSSQL;

public class MaterialRepositorio : RepositorioMSSQL
{
    public MaterialRepositorio(string cadenaConexionBD) : base(cadenaConexionBD) { }

    public Object Listar(Guid idUsuario, ParmMaterialFiltro filtro)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@user_id", idUsuario);
        parametros.Add("@branch_id", filtro.branch_id);
        parametros.Add("@include_inactive", filtro.include_inactive);
        return base.Listar("api.usp_materials_list", parametros);
    }

    public Object StockSucursales(Guid idUsuario, Guid idMaterial)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@user_id", idUsuario);
        parametros.Add("@material_id", idMaterial);
        return base.Listar("api.usp_material_stock_by_branch", parametros);
    }

    public RespuestaProceso MaterialPrc(Guid idUsuario, ParmMaterial entidad)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@user_id", idUsuario);
        parametros.Add("@id", entidad.id);
        parametros.Add("@name", entidad.name);
        parametros.Add("@type", entidad.type);
        parametros.Add("@purity", entidad.purity);
        parametros.Add("@unit", entidad.unit);
        parametros.Add("@min_stock", entidad.min_stock);
        parametros.Add("@reference_cost", entidad.reference_cost);
        parametros.Add("@active", entidad.active);
        parametros.Add("@initial_stock", entidad.initial_stock);
        parametros.Add("@branch_id", entidad.branch_id);
        return base.Procesar("api.usp_material_save", parametros);
    }

    public RespuestaProceso RegistrarMovimiento(Guid idUsuario, ParmMovimiento entidad)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@user_id", idUsuario);
        parametros.Add("@material_id", entidad.material_id);
        parametros.Add("@movement_type", entidad.movement_type);
        parametros.Add("@quantity", entidad.quantity);
        parametros.Add("@ownership", entidad.ownership);
        parametros.Add("@order_id", entidad.order_id);
        parametros.Add("@branch_id", entidad.branch_id);
        parametros.Add("@unit_cost", entidad.unit_cost);
        parametros.Add("@movement_date", entidad.movement_date);
        parametros.Add("@notes", entidad.notes);
        return base.Procesar("api.usp_material_movement_register", parametros);
    }

    public RespuestaProceso Trasladar(Guid idUsuario, ParmTraslado entidad)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@user_id", idUsuario);
        parametros.Add("@material_id", entidad.material_id);
        parametros.Add("@from_branch_id", entidad.from_branch_id);
        parametros.Add("@to_branch_id", entidad.to_branch_id);
        parametros.Add("@quantity", entidad.quantity);
        parametros.Add("@notes", entidad.notes);
        return base.Procesar("api.usp_material_transfer", parametros);
    }

    public RespuestaProceso StockMinimo(Guid idUsuario, ParmStockMinimo entidad)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@user_id", idUsuario);
        parametros.Add("@material_id", entidad.material_id);
        parametros.Add("@branch_id", entidad.branch_id);
        parametros.Add("@min_stock", entidad.min_stock);
        return base.Procesar("api.usp_material_set_branch_min_stock", parametros);
    }

    public Object Kardex(Guid idUsuario, ParmKardex filtro)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@user_id", idUsuario);
        parametros.Add("@material_id", filtro.material_id);
        parametros.Add("@branch_id", filtro.branch_id);
        parametros.Add("@from", filtro.from);
        parametros.Add("@to", filtro.to);
        return base.Listar("api.usp_material_kardex", parametros);
    }

    public Object MaterialCliente(Guid idUsuario, Guid idPedido)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@user_id", idUsuario);
        parametros.Add("@order_id", idPedido);
        return base.Listar("api.usp_order_client_materials", parametros);
    }
}
