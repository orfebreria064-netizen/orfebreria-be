using AURUM.Aplicacion.Base;
using AURUM.Modelos.Aplicacion;
using AURUM.Modelos.Reportes;
using AURUM.RepositorioMSSQL;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace AURUM.Aplicacion.Reportes;

// Dashboard gerencial y reportes (ADMIN y GERENTE; el procedimiento lo valida)
public class ReporteAPL : AplicacionBase
{
    public ReporteAPL(IConfiguration iConfiguration, IOptions<ConfiguracionAPP> iConfiguracionAPP) : base(iConfiguration, iConfiguracionAPP)
    { }

    public Object? Dashboard(Guid idUsuario, Guid? idSucursal)
    {
        ReporteRepositorio _repositorio = new ReporteRepositorio(parametrosApp.BD);
        return _repositorio.Dashboard(idUsuario, idSucursal);
    }

    public Object Ventas(Guid idUsuario, ParmReporteVentas filtro)
    {
        ReporteRepositorio _repositorio = new ReporteRepositorio(parametrosApp.BD);
        return _repositorio.Ventas(idUsuario, filtro);
    }

    public Object CuentasPorCobrar(Guid idUsuario, ParmReporteCuentasCobrar filtro)
    {
        ReporteRepositorio _repositorio = new ReporteRepositorio(parametrosApp.BD);
        return _repositorio.CuentasPorCobrar(idUsuario, filtro);
    }

    public Object Cobranza(Guid idUsuario, ParmReporteCobranza filtro)
    {
        ReporteRepositorio _repositorio = new ReporteRepositorio(parametrosApp.BD);
        return _repositorio.Cobranza(idUsuario, filtro);
    }

    public Object Plazos(Guid idUsuario, ParmReportePlazos filtro)
    {
        ReporteRepositorio _repositorio = new ReporteRepositorio(parametrosApp.BD);
        return _repositorio.Plazos(idUsuario, filtro);
    }

    public Object Stock(Guid idUsuario, ParmReporteStock filtro)
    {
        ReporteRepositorio _repositorio = new ReporteRepositorio(parametrosApp.BD);
        return _repositorio.Stock(idUsuario, filtro);
    }

    public Object Custodia(Guid idUsuario, ParmReporteCustodia filtro)
    {
        ReporteRepositorio _repositorio = new ReporteRepositorio(parametrosApp.BD);
        return _repositorio.Custodia(idUsuario, filtro);
    }
}
