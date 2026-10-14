using AURUM.Aplicacion.Base;
using AURUM.Modelos.Aplicacion;
using AURUM.Modelos.Materiales;
using AURUM.RepositorioMSSQL;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace AURUM.Aplicacion.Almacen;

// Materiales, stock por sucursal, movimientos, traslados y kárdex
public class MaterialAPL : AplicacionBase
{
    public MaterialAPL(IConfiguration iConfiguration, IOptions<ConfiguracionAPP> iConfiguracionAPP) : base(iConfiguration, iConfiguracionAPP)
    { }

    public Object Listar(Guid idUsuario, ParmMaterialFiltro filtro)
    {
        MaterialRepositorio _repositorio = new MaterialRepositorio(parametrosApp.BD);
        return _repositorio.Listar(idUsuario, filtro);
    }

    public Object StockSucursales(Guid idUsuario, Guid idMaterial)
    {
        MaterialRepositorio _repositorio = new MaterialRepositorio(parametrosApp.BD);
        return _repositorio.StockSucursales(idUsuario, idMaterial);
    }

    public RespuestaProceso MaterialPrc(Guid idUsuario, ParmMaterial entidad)
    {
        MaterialRepositorio _repositorio = new MaterialRepositorio(parametrosApp.BD);
        return _repositorio.MaterialPrc(idUsuario, entidad);
    }

    public RespuestaProceso RegistrarMovimiento(Guid idUsuario, ParmMovimiento entidad)
    {
        MaterialRepositorio _repositorio = new MaterialRepositorio(parametrosApp.BD);
        return _repositorio.RegistrarMovimiento(idUsuario, entidad);
    }

    public RespuestaProceso Trasladar(Guid idUsuario, ParmTraslado entidad)
    {
        MaterialRepositorio _repositorio = new MaterialRepositorio(parametrosApp.BD);
        return _repositorio.Trasladar(idUsuario, entidad);
    }

    public RespuestaProceso StockMinimo(Guid idUsuario, ParmStockMinimo entidad)
    {
        MaterialRepositorio _repositorio = new MaterialRepositorio(parametrosApp.BD);
        return _repositorio.StockMinimo(idUsuario, entidad);
    }

    public Object Kardex(Guid idUsuario, ParmKardex filtro)
    {
        MaterialRepositorio _repositorio = new MaterialRepositorio(parametrosApp.BD);
        return _repositorio.Kardex(idUsuario, filtro);
    }

    public Object MaterialCliente(Guid idUsuario, Guid idPedido)
    {
        MaterialRepositorio _repositorio = new MaterialRepositorio(parametrosApp.BD);
        return _repositorio.MaterialCliente(idUsuario, idPedido);
    }
}
