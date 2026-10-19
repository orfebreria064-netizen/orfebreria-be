using AURUM.Aplicacion.Base;
using AURUM.Modelos.Aplicacion;
using AURUM.Modelos.Produccion;
using AURUM.RepositorioMSSQL;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace AURUM.Aplicacion.Produccion;

// Joyeros externos y sus recibos de egreso (pagos por pedido)
public class JoyeroAPL : AplicacionBase
{
    public JoyeroAPL(IConfiguration iConfiguration, IOptions<ConfiguracionAPP> iConfiguracionAPP) : base(iConfiguration, iConfiguracionAPP)
    { }

    public Object Listar(Guid idUsuario, bool incluirInactivos)
    {
        JoyeroRepositorio _repositorio = new JoyeroRepositorio(parametrosApp.BD);
        return _repositorio.Listar(idUsuario, incluirInactivos);
    }

    public RespuestaProceso JoyeroPrc(Guid idUsuario, ParmJoyero entidad)
    {
        JoyeroRepositorio _repositorio = new JoyeroRepositorio(parametrosApp.BD);
        return _repositorio.JoyeroPrc(idUsuario, entidad);
    }

    public RespuestaProceso RegistrarPago(Guid idUsuario, ParmPagoJoyero entidad)
    {
        JoyeroRepositorio _repositorio = new JoyeroRepositorio(parametrosApp.BD);
        return _repositorio.RegistrarPago(idUsuario, entidad);
    }

    public RespuestaProceso AnularPago(Guid idUsuario, ParmAnularPagoJoyero entidad)
    {
        JoyeroRepositorio _repositorio = new JoyeroRepositorio(parametrosApp.BD);
        return _repositorio.AnularPago(idUsuario, entidad);
    }

    public Object ListarPagos(Guid idUsuario, ParmPagoJoyeroFiltro filtro)
    {
        JoyeroRepositorio _repositorio = new JoyeroRepositorio(parametrosApp.BD);
        return _repositorio.ListarPagos(idUsuario, filtro);
    }
}
