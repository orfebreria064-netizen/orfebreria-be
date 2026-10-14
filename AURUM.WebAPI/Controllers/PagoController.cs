using AURUM.Aplicacion.Ventas;
using AURUM.Modelos.Aplicacion;
using AURUM.Modelos.Ventas;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace AURUM.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class PagoController : ControladorBase
    {
        public PagoController(ILogger<PagoController> _logger, IConfiguration _configuration,
            IOptions<ConfiguracionAPP> _configuracionAPP) : base(_configuration, _configuracionAPP, _logger) { }

        [HttpPost]
        [Route("registrar")]
        public ActionResult Registrar([FromBody] ParmPago entidad)
        {
            PagoAPL _apl = new PagoAPL(configuration, configuracionAPP);
            return Procesar(() => _apl.Registrar(IdUsuario, entidad));
        }

        [HttpPost]
        [Route("anular")]
        public ActionResult Anular([FromBody] ParmAnularPago entidad)
        {
            PagoAPL _apl = new PagoAPL(configuration, configuracionAPP);
            return Procesar(() => _apl.Anular(IdUsuario, entidad));
        }

        [HttpPost]
        [Route("listarpagos")]
        public ActionResult ListarPagos([FromBody] ParmPagoFiltro filtro)
        {
            PagoAPL _apl = new PagoAPL(configuration, configuracionAPP);
            return Consultar(() => _apl.Listar(IdUsuario, filtro));
        }

        // Pedidos con saldo pendiente (cobranza)
        [HttpGet]
        [Route("porcobrar")]
        public ActionResult PorCobrar([FromQuery] Guid? idsucursal)
        {
            PagoAPL _apl = new PagoAPL(configuration, configuracionAPP);
            return Consultar(() => _apl.PorCobrar(IdUsuario, idsucursal));
        }
    }
}
