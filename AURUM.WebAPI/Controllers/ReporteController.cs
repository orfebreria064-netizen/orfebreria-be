using AURUM.Aplicacion.Reportes;
using AURUM.Modelos.Aplicacion;
using AURUM.Modelos.Reportes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace AURUM.WebAPI.Controllers
{
    // Dashboard gerencial y reportes
    [Route("api/[controller]")]
    public class ReporteController : ControladorBase
    {
        public ReporteController(ILogger<ReporteController> _logger, IConfiguration _configuration,
            IOptions<ConfiguracionAPP> _configuracionAPP) : base(_configuration, _configuracionAPP, _logger) { }

        // Mismo JSON que get_dashboard() de Supabase. ?idsucursal= opcional
        [HttpGet]
        [Route("dashboard")]
        public ActionResult Dashboard([FromQuery] Guid? idsucursal)
        {
            ReporteAPL _apl = new ReporteAPL(configuration, configuracionAPP);
            return Consultar(() => _apl.Dashboard(IdUsuario, idsucursal));
        }

        [HttpPost]
        [Route("ventas")]
        public ActionResult Ventas([FromBody] ParmReporteVentas filtro)
        {
            ReporteAPL _apl = new ReporteAPL(configuration, configuracionAPP);
            return Consultar(() => _apl.Ventas(IdUsuario, filtro));
        }

        [HttpPost]
        [Route("cuentasporcobrar")]
        public ActionResult CuentasPorCobrar([FromBody] ParmReporteCuentasCobrar filtro)
        {
            ReporteAPL _apl = new ReporteAPL(configuration, configuracionAPP);
            return Consultar(() => _apl.CuentasPorCobrar(IdUsuario, filtro));
        }

        [HttpPost]
        [Route("cobranza")]
        public ActionResult Cobranza([FromBody] ParmReporteCobranza filtro)
        {
            ReporteAPL _apl = new ReporteAPL(configuration, configuracionAPP);
            return Consultar(() => _apl.Cobranza(IdUsuario, filtro));
        }

        [HttpPost]
        [Route("plazos")]
        public ActionResult Plazos([FromBody] ParmReportePlazos filtro)
        {
            ReporteAPL _apl = new ReporteAPL(configuration, configuracionAPP);
            return Consultar(() => _apl.Plazos(IdUsuario, filtro));
        }

        [HttpPost]
        [Route("stock")]
        public ActionResult Stock([FromBody] ParmReporteStock filtro)
        {
            ReporteAPL _apl = new ReporteAPL(configuration, configuracionAPP);
            return Consultar(() => _apl.Stock(IdUsuario, filtro));
        }

        [HttpPost]
        [Route("custodia")]
        public ActionResult Custodia([FromBody] ParmReporteCustodia filtro)
        {
            ReporteAPL _apl = new ReporteAPL(configuration, configuracionAPP);
            return Consultar(() => _apl.Custodia(IdUsuario, filtro));
        }
    }
}
