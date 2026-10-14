using AURUM.Aplicacion.Base;
using AURUM.Modelos.Aplicacion;
using AURUM.Modelos.Ventas;
using AURUM.RepositorioMSSQL;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace AURUM.Aplicacion.Ventas;

// Pedidos y producción
public class PedidoAPL : AplicacionBase
{
    public PedidoAPL(IConfiguration iConfiguration, IOptions<ConfiguracionAPP> iConfiguracionAPP) : base(iConfiguration, iConfiguracionAPP)
    { }

    public Object Listar(Guid idUsuario, ParmPedidoFiltro filtro)
    {
        PedidoRepositorio _repositorio = new PedidoRepositorio(parametrosApp.BD);
        return _repositorio.Listar(idUsuario, filtro);
    }

    public Object TraerUno(Guid idUsuario, Guid idPedido)
    {
        PedidoRepositorio _repositorio = new PedidoRepositorio(parametrosApp.BD);
        return _repositorio.TraerUno(idUsuario, idPedido);
    }

    public RespuestaProceso PedidoPrc(Guid idUsuario, ParmPedido entidad)
    {
        PedidoRepositorio _repositorio = new PedidoRepositorio(parametrosApp.BD);
        return _repositorio.PedidoPrc(idUsuario, entidad);
    }

    public RespuestaProceso CambiarEstado(Guid idUsuario, ParmCambioEstado entidad)
    {
        PedidoRepositorio _repositorio = new PedidoRepositorio(parametrosApp.BD);
        return _repositorio.CambiarEstado(idUsuario, entidad);
    }

    public RespuestaProceso Asignar(Guid idUsuario, ParmAsignar entidad)
    {
        PedidoRepositorio _repositorio = new PedidoRepositorio(parametrosApp.BD);
        return _repositorio.Asignar(idUsuario, entidad);
    }

    public RespuestaProceso Cronograma(Guid idUsuario, ParmCronograma entidad)
    {
        PedidoRepositorio _repositorio = new PedidoRepositorio(parametrosApp.BD);
        return _repositorio.Cronograma(idUsuario, entidad);
    }

    public RespuestaProceso GenerarEtapas(Guid idUsuario, Guid idPedido)
    {
        PedidoRepositorio _repositorio = new PedidoRepositorio(parametrosApp.BD);
        return _repositorio.GenerarEtapas(idUsuario, idPedido);
    }

    public RespuestaProceso AgregarEtapa(Guid idUsuario, ParmEtapaAgregar entidad)
    {
        PedidoRepositorio _repositorio = new PedidoRepositorio(parametrosApp.BD);
        return _repositorio.AgregarEtapa(idUsuario, entidad);
    }

    public RespuestaProceso ActualizarEtapa(Guid idUsuario, ParmEtapaActualizar entidad)
    {
        PedidoRepositorio _repositorio = new PedidoRepositorio(parametrosApp.BD);
        return _repositorio.ActualizarEtapa(idUsuario, entidad);
    }

    public RespuestaProceso EliminarEtapa(Guid idUsuario, Guid idEtapa)
    {
        PedidoRepositorio _repositorio = new PedidoRepositorio(parametrosApp.BD);
        return _repositorio.EliminarEtapa(idUsuario, idEtapa);
    }

    public Object Produccion(Guid idUsuario, ParmProduccionFiltro filtro)
    {
        PedidoRepositorio _repositorio = new PedidoRepositorio(parametrosApp.BD);
        return _repositorio.Produccion(idUsuario, filtro);
    }
}
