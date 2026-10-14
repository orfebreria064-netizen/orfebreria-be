using AURUM.Modelos.Aplicacion;
using AURUM.Modelos.Configuracion;
using Dapper;

namespace AURUM.RepositorioMSSQL;

public class ConfiguracionRepositorio : RepositorioMSSQL
{
    public ConfiguracionRepositorio(string cadenaConexionBD) : base(cadenaConexionBD) { }

    public Object? TraerConfiguracion(Guid idUsuario)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@user_id", idUsuario);
        return base.TraerUno("api.usp_settings_get", parametros);
    }

    public RespuestaProceso ActualizarConfiguracion(Guid idUsuario, ParmConfiguracion entidad)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@user_id", idUsuario);
        parametros.Add("@company_name", entidad.company_name);
        parametros.Add("@tax_id", entidad.tax_id);
        parametros.Add("@address", entidad.address);
        parametros.Add("@phone", entidad.phone);
        parametros.Add("@email", entidad.email);
        parametros.Add("@currency", entidad.currency);
        parametros.Add("@tax_rate", entidad.tax_rate);
        parametros.Add("@quote_validity_days", entidad.quote_validity_days);
        parametros.Add("@default_terms", entidad.default_terms);
        return base.Procesar("api.usp_settings_update", parametros);
    }

    public Object ListarServicios(Guid idUsuario, bool incluirInactivos)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@user_id", idUsuario);
        parametros.Add("@include_inactive", incluirInactivos);
        return base.Listar("api.usp_service_types_list", parametros);
    }

    public RespuestaProceso ServicioPrc(Guid idUsuario, ParmServicio entidad)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@user_id", idUsuario);
        parametros.Add("@id", entidad.id);
        parametros.Add("@code", entidad.code);
        parametros.Add("@name", entidad.name);
        parametros.Add("@position", entidad.position);
        parametros.Add("@active", entidad.active);
        return base.Procesar("api.usp_service_type_save", parametros);
    }

    public Object ListarEtapas(Guid idUsuario, bool incluirInactivos)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@user_id", idUsuario);
        parametros.Add("@include_inactive", incluirInactivos);
        return base.Listar("api.usp_production_stages_list", parametros);
    }

    public RespuestaProceso EtapaPrc(Guid idUsuario, ParmEtapa entidad)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@user_id", idUsuario);
        parametros.Add("@id", entidad.id);
        parametros.Add("@name", entidad.name);
        parametros.Add("@position", entidad.position);
        parametros.Add("@is_quality_check", entidad.is_quality_check);
        parametros.Add("@active", entidad.active);
        return base.Procesar("api.usp_production_stage_save", parametros);
    }

    public Object ListarSucursales(Guid idUsuario)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@user_id", idUsuario);
        return base.Listar("api.usp_branches_list", parametros);
    }

    public RespuestaProceso SucursalPrc(Guid idUsuario, ParmSucursal entidad)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@user_id", idUsuario);
        parametros.Add("@id", entidad.id);
        parametros.Add("@code", entidad.code);
        parametros.Add("@name", entidad.name);
        parametros.Add("@address", entidad.address);
        parametros.Add("@phone", entidad.phone);
        parametros.Add("@active", entidad.active);
        return base.Procesar("api.usp_branch_save", parametros);
    }

    public RespuestaProceso SucursalPrincipal(Guid idUsuario, Guid idSucursal)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@user_id", idUsuario);
        parametros.Add("@branch_id", idSucursal);
        return base.Procesar("api.usp_branch_set_main", parametros);
    }
}
