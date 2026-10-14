using AURUM.Aplicacion.Ventas;
using AURUM.Modelos.Aplicacion;
using AURUM.Modelos.Ventas;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace AURUM.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class CotizacionController : ControladorBase
    {
        public CotizacionController(ILogger<CotizacionController> _logger, IConfiguration _configuration,
            IOptions<ConfiguracionAPP> _configuracionAPP) : base(_configuration, _configuracionAPP, _logger) { }

        [HttpPost]
        [Route("listarcotizaciones")]
        public ActionResult ListarCotizaciones([FromBody] ParmCotizacionFiltro filtro)
        {
            CotizacionAPL _apl = new CotizacionAPL(configuration, configuracionAPP);
            return Consultar(() => _apl.Listar(IdUsuario, filtro));
        }

        [HttpGet]
        [Route("traeruno/{idcotizacion}")]
        public ActionResult TraerUno(Guid idcotizacion)
        {
            CotizacionAPL _apl = new CotizacionAPL(configuration, configuracionAPP);
            return Consultar(() => _apl.TraerUno(IdUsuario, idcotizacion));
        }

        [HttpPost]
        [Route("cotizacionprc")]
        public ActionResult CotizacionPrc([FromBody] ParmCotizacion entidad)
        {
            CotizacionAPL _apl = new CotizacionAPL(configuration, configuracionAPP);
            return Procesar(() => _apl.CotizacionPrc(IdUsuario, entidad));
        }

        [HttpPost]
        [Route("cambiarestado")]
        public ActionResult CambiarEstado([FromBody] ParmCambioEstado entidad)
        {
            CotizacionAPL _apl = new CotizacionAPL(configuration, configuracionAPP);
            return Procesar(() => _apl.CambiarEstado(IdUsuario, entidad));
        }

        [HttpPost]
        [Route("duplicar/{idcotizacion}")]
        public ActionResult Duplicar(Guid idcotizacion)
        {
            CotizacionAPL _apl = new CotizacionAPL(configuration, configuracionAPP);
            return Procesar(() => _apl.Duplicar(IdUsuario, idcotizacion));
        }

        [HttpPost]
        [Route("vencer")]
        public ActionResult Vencer()
        {
            CotizacionAPL _apl = new CotizacionAPL(configuration, configuracionAPP);
            return Procesar(() => _apl.Vencer(IdUsuario));
        }

        [HttpPost]
        [Route("convertirpedido")]
        public ActionResult ConvertirPedido([FromBody] ParmConvertirPedido entidad)
        {
            CotizacionAPL _apl = new CotizacionAPL(configuration, configuracionAPP);
            return Procesar(() => _apl.ConvertirPedido(IdUsuario, entidad));
        }
    }
}
