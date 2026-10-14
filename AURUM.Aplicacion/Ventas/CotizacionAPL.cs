using AURUM.Aplicacion.Base;
using AURUM.Modelos.Aplicacion;
using AURUM.Modelos.Ventas;
using AURUM.RepositorioMSSQL;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace AURUM.Aplicacion.Ventas;

public class CotizacionAPL : AplicacionBase
{
    public CotizacionAPL(IConfiguration iConfiguration, IOptions<ConfiguracionAPP> iConfiguracionAPP) : base(iConfiguration, iConfiguracionAPP)
    { }

    public Object Listar(Guid idUsuario, ParmCotizacionFiltro filtro)
    {
        CotizacionRepositorio _repositorio = new CotizacionRepositorio(parametrosApp.BD);
        return _repositorio.Listar(idUsuario, filtro);
    }

    public Object TraerUno(Guid idUsuario, Guid idCotizacion)
    {
        CotizacionRepositorio _repositorio = new CotizacionRepositorio(parametrosApp.BD);
        return _repositorio.TraerUno(idUsuario, idCotizacion);
    }

    public RespuestaProceso CotizacionPrc(Guid idUsuario, ParmCotizacion entidad)
    {
        CotizacionRepositorio _repositorio = new CotizacionRepositorio(parametrosApp.BD);
        return _repositorio.CotizacionPrc(idUsuario, entidad);
    }

    public RespuestaProceso CambiarEstado(Guid idUsuario, ParmCambioEstado entidad)
    {
        CotizacionRepositorio _repositorio = new CotizacionRepositorio(parametrosApp.BD);
        return _repositorio.CambiarEstado(idUsuario, entidad);
    }

    public RespuestaProceso Duplicar(Guid idUsuario, Guid idCotizacion)
    {
        CotizacionRepositorio _repositorio = new CotizacionRepositorio(parametrosApp.BD);
        return _repositorio.Duplicar(idUsuario, idCotizacion);
    }

    public RespuestaProceso Vencer(Guid idUsuario)
    {
        CotizacionRepositorio _repositorio = new CotizacionRepositorio(parametrosApp.BD);
        return _repositorio.Vencer(idUsuario);
    }

    public RespuestaProceso ConvertirPedido(Guid idUsuario, ParmConvertirPedido entidad)
    {
        CotizacionRepositorio _repositorio = new CotizacionRepositorio(parametrosApp.BD);
        return _repositorio.ConvertirPedido(idUsuario, entidad);
    }
}
