using AURUM.Aplicacion.Base;
using AURUM.Modelos.Aplicacion;
using AURUM.Modelos.Ventas;
using AURUM.RepositorioMSSQL;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace AURUM.Aplicacion.Ventas;

// Pagos y entregas
public class PagoAPL : AplicacionBase
{
    public PagoAPL(IConfiguration iConfiguration, IOptions<ConfiguracionAPP> iConfiguracionAPP) : base(iConfiguration, iConfiguracionAPP)
    { }

    public RespuestaProceso Registrar(Guid idUsuario, ParmPago entidad)
    {
        PagoRepositorio _repositorio = new PagoRepositorio(parametrosApp.BD);
        return _repositorio.Registrar(idUsuario, entidad);
    }

    public RespuestaProceso EmitirRecibo(Guid idUsuario, Guid idPago)
    {
        PagoRepositorio _repositorio = new PagoRepositorio(parametrosApp.BD);
        return _repositorio.EmitirRecibo(idUsuario, idPago);
    }

    public RespuestaProceso Anular(Guid idUsuario, ParmAnularPago entidad)
    {
        PagoRepositorio _repositorio = new PagoRepositorio(parametrosApp.BD);
        return _repositorio.Anular(idUsuario, entidad);
    }

    public Object Listar(Guid idUsuario, ParmPagoFiltro filtro)
    {
        PagoRepositorio _repositorio = new PagoRepositorio(parametrosApp.BD);
        return _repositorio.Listar(idUsuario, filtro);
    }

    public Object PorCobrar(Guid idUsuario, Guid? idSucursal)
    {
        PagoRepositorio _repositorio = new PagoRepositorio(parametrosApp.BD);
        return _repositorio.PorCobrar(idUsuario, idSucursal);
    }

    public RespuestaProceso EntregaPrc(Guid idUsuario, ParmEntrega entidad)
    {
        EntregaRepositorio _repositorio = new EntregaRepositorio(parametrosApp.BD);
        return _repositorio.EntregaPrc(idUsuario, entidad);
    }

    public Object ListarEntregas(Guid idUsuario, ParmEntregaFiltro filtro)
    {
        EntregaRepositorio _repositorio = new EntregaRepositorio(parametrosApp.BD);
        return _repositorio.Listar(idUsuario, filtro);
    }
}
