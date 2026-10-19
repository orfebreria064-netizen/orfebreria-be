using AURUM.Aplicacion.Produccion;
using AURUM.Modelos.Aplicacion;
using AURUM.Modelos.Produccion;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace AURUM.WebAPI.Controllers
{
    // Joyeros externos y recibos de egreso (pagos a joyeros por pedido)
    [Route("api/[controller]")]
    public class JoyeroController : ControladorBase
    {
        public JoyeroController(ILogger<JoyeroController> _logger, IConfiguration _configuration,
            IOptions<ConfiguracionAPP> _configuracionAPP) : base(_configuration, _configuracionAPP, _logger) { }

        [HttpGet]
        [Route("listarjoyeros")]
        public ActionResult ListarJoyeros([FromQuery] bool incluirinactivos = false)
        {
            JoyeroAPL _apl = new JoyeroAPL(configuration, configuracionAPP);
            return Consultar(() => _apl.Listar(IdUsuario, incluirinactivos));
        }

        [HttpPost]
        [Route("joyeroprc")]
        public ActionResult JoyeroPrc([FromBody] ParmJoyero entidad)
        {
            JoyeroAPL _apl = new JoyeroAPL(configuration, configuracionAPP);
            return Procesar(() => _apl.JoyeroPrc(IdUsuario, entidad));
        }

        [HttpPost]
        [Route("registrarpago")]
        public ActionResult RegistrarPago([FromBody] ParmPagoJoyero entidad)
        {
            JoyeroAPL _apl = new JoyeroAPL(configuration, configuracionAPP);
            return Procesar(() => _apl.RegistrarPago(IdUsuario, entidad));
        }

        [HttpPost]
        [Route("anularpago")]
        public ActionResult AnularPago([FromBody] ParmAnularPagoJoyero entidad)
        {
            JoyeroAPL _apl = new JoyeroAPL(configuration, configuracionAPP);
            return Procesar(() => _apl.AnularPago(IdUsuario, entidad));
        }

        [HttpPost]
        [Route("listarpagos")]
        public ActionResult ListarPagos([FromBody] ParmPagoJoyeroFiltro filtro)
        {
            JoyeroAPL _apl = new JoyeroAPL(configuration, configuracionAPP);
            return Consultar(() => _apl.ListarPagos(IdUsuario, filtro));
        }
    }
}
