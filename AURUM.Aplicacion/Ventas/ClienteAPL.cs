using AURUM.Aplicacion.Base;
using AURUM.Modelos.Aplicacion;
using AURUM.Modelos.Clientes;
using AURUM.RepositorioMSSQL;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace AURUM.Aplicacion.Ventas;

public class ClienteAPL : AplicacionBase
{
    public ClienteAPL(IConfiguration iConfiguration, IOptions<ConfiguracionAPP> iConfiguracionAPP) : base(iConfiguration, iConfiguracionAPP)
    { }

    public Object Listar(Guid idUsuario, ParmClienteFiltro filtro)
    {
        ClienteRepositorio _repositorio = new ClienteRepositorio(parametrosApp.BD);
        return _repositorio.Listar(idUsuario, filtro);
    }

    public Object TraerUno(Guid idUsuario, Guid idCliente)
    {
        ClienteRepositorio _repositorio = new ClienteRepositorio(parametrosApp.BD);
        return _repositorio.TraerUno(idUsuario, idCliente);
    }

    public Object Historial(Guid idUsuario, Guid idCliente, int limite)
    {
        ClienteRepositorio _repositorio = new ClienteRepositorio(parametrosApp.BD);
        return _repositorio.Historial(idUsuario, idCliente, limite);
    }

    public RespuestaProceso ClientePrc(Guid idUsuario, ParmCliente entidad)
    {
        ClienteRepositorio _repositorio = new ClienteRepositorio(parametrosApp.BD);
        return _repositorio.ClientePrc(idUsuario, entidad);
    }

    public RespuestaProceso ContactoPrc(Guid idUsuario, ParmContacto entidad)
    {
        ClienteRepositorio _repositorio = new ClienteRepositorio(parametrosApp.BD);
        return _repositorio.ContactoPrc(idUsuario, entidad);
    }

    public RespuestaProceso EliminarContacto(Guid idUsuario, Guid idContacto)
    {
        ClienteRepositorio _repositorio = new ClienteRepositorio(parametrosApp.BD);
        return _repositorio.EliminarContacto(idUsuario, idContacto);
    }
}
