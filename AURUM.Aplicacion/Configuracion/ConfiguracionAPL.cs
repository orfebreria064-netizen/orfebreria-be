using AURUM.Aplicacion.Base;
using AURUM.Modelos.Aplicacion;
using AURUM.Modelos.Configuracion;
using AURUM.RepositorioMSSQL;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace AURUM.Aplicacion.Configuracion;

// Datos de la empresa, tipos de servicio, plantilla de etapas y sucursales
public class ConfiguracionAPL : AplicacionBase
{
    public ConfiguracionAPL(IConfiguration iConfiguration, IOptions<ConfiguracionAPP> iConfiguracionAPP) : base(iConfiguration, iConfiguracionAPP)
    { }

    public Object? TraerConfiguracion(Guid idUsuario)
    {
        ConfiguracionRepositorio _repositorio = new ConfiguracionRepositorio(parametrosApp.BD);
        return _repositorio.TraerConfiguracion(idUsuario);
    }

    public RespuestaProceso ActualizarConfiguracion(Guid idUsuario, ParmConfiguracion entidad)
    {
        ConfiguracionRepositorio _repositorio = new ConfiguracionRepositorio(parametrosApp.BD);
        return _repositorio.ActualizarConfiguracion(idUsuario, entidad);
    }

    public Object ListarServicios(Guid idUsuario, bool incluirInactivos)
    {
        ConfiguracionRepositorio _repositorio = new ConfiguracionRepositorio(parametrosApp.BD);
        return _repositorio.ListarServicios(idUsuario, incluirInactivos);
    }

    public RespuestaProceso ServicioPrc(Guid idUsuario, ParmServicio entidad)
    {
        ConfiguracionRepositorio _repositorio = new ConfiguracionRepositorio(parametrosApp.BD);
        return _repositorio.ServicioPrc(idUsuario, entidad);
    }

    public Object ListarEtapas(Guid idUsuario, bool incluirInactivos)
    {
        ConfiguracionRepositorio _repositorio = new ConfiguracionRepositorio(parametrosApp.BD);
        return _repositorio.ListarEtapas(idUsuario, incluirInactivos);
    }

    public RespuestaProceso EtapaPrc(Guid idUsuario, ParmEtapa entidad)
    {
        ConfiguracionRepositorio _repositorio = new ConfiguracionRepositorio(parametrosApp.BD);
        return _repositorio.EtapaPrc(idUsuario, entidad);
    }

    public Object ListarSucursales(Guid idUsuario)
    {
        ConfiguracionRepositorio _repositorio = new ConfiguracionRepositorio(parametrosApp.BD);
        return _repositorio.ListarSucursales(idUsuario);
    }

    public RespuestaProceso SucursalPrc(Guid idUsuario, ParmSucursal entidad)
    {
        ConfiguracionRepositorio _repositorio = new ConfiguracionRepositorio(parametrosApp.BD);
        return _repositorio.SucursalPrc(idUsuario, entidad);
    }

    public RespuestaProceso SucursalPrincipal(Guid idUsuario, Guid idSucursal)
    {
        ConfiguracionRepositorio _repositorio = new ConfiguracionRepositorio(parametrosApp.BD);
        return _repositorio.SucursalPrincipal(idUsuario, idSucursal);
    }
}
