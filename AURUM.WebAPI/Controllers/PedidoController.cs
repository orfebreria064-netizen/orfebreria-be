using AURUM.Aplicacion.Ventas;
using AURUM.Modelos.Aplicacion;
using AURUM.Modelos.Ventas;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace AURUM.WebAPI.Controllers
{
    // Pedidos y producción
    [Route("api/[controller]")]
    public class PedidoController : ControladorBase
    {
        public PedidoController(ILogger<PedidoController> _logger, IConfiguration _configuration,
            IOptions<ConfiguracionAPP> _configuracionAPP) : base(_configuration, _configuracionAPP, _logger) { }

        [HttpPost]
        [Route("listarpedidos")]
        public ActionResult ListarPedidos([FromBody] ParmPedidoFiltro filtro)
        {
            PedidoAPL _apl = new PedidoAPL(configuration, configuracionAPP);
            return Consultar(() => _apl.Listar(IdUsuario, filtro));
        }

        [HttpGet]
        [Route("traeruno/{idpedido}")]
        public ActionResult TraerUno(Guid idpedido)
        {
            PedidoAPL _apl = new PedidoAPL(configuration, configuracionAPP);
            return Consultar(() => _apl.TraerUno(IdUsuario, idpedido));
        }

        [HttpPost]
        [Route("pedidoprc")]
        public ActionResult PedidoPrc([FromBody] ParmPedido entidad)
        {
            PedidoAPL _apl = new PedidoAPL(configuration, configuracionAPP);
            return Procesar(() => _apl.PedidoPrc(IdUsuario, entidad));
        }

        [HttpPost]
        [Route("cambiarestado")]
        public ActionResult CambiarEstado([FromBody] ParmCambioEstado entidad)
        {
            PedidoAPL _apl = new PedidoAPL(configuration, configuracionAPP);
            return Procesar(() => _apl.CambiarEstado(IdUsuario, entidad));
        }

        [HttpPost]
        [Route("asignar")]
        public ActionResult Asignar([FromBody] ParmAsignar entidad)
        {
            PedidoAPL _apl = new PedidoAPL(configuration, configuracionAPP);
            return Procesar(() => _apl.Asignar(IdUsuario, entidad));
        }

        [HttpPost]
        [Route("cronograma")]
        public ActionResult Cronograma([FromBody] ParmCronograma entidad)
        {
            PedidoAPL _apl = new PedidoAPL(configuration, configuracionAPP);
            return Procesar(() => _apl.Cronograma(IdUsuario, entidad));
        }

        [HttpPost]
        [Route("generaretapas/{idpedido}")]
        public ActionResult GenerarEtapas(Guid idpedido)
        {
            PedidoAPL _apl = new PedidoAPL(configuration, configuracionAPP);
            return Procesar(() => _apl.GenerarEtapas(IdUsuario, idpedido));
        }

        [HttpPost]
        [Route("agregaretapa")]
        public ActionResult AgregarEtapa([FromBody] ParmEtapaAgregar entidad)
        {
            PedidoAPL _apl = new PedidoAPL(configuration, configuracionAPP);
            return Procesar(() => _apl.AgregarEtapa(IdUsuario, entidad));
        }

        [HttpPost]
        [Route("actualizaretapa")]
        public ActionResult ActualizarEtapa([FromBody] ParmEtapaActualizar entidad)
        {
            PedidoAPL _apl = new PedidoAPL(configuration, configuracionAPP);
            return Procesar(() => _apl.ActualizarEtapa(IdUsuario, entidad));
        }

        [HttpPost]
        [Route("eliminaretapa/{idetapa}")]
        public ActionResult EliminarEtapa(Guid idetapa)
        {
            PedidoAPL _apl = new PedidoAPL(configuration, configuracionAPP);
            return Procesar(() => _apl.EliminarEtapa(IdUsuario, idetapa));
        }

        [HttpPost]
        [Route("produccion")]
        public ActionResult Produccion([FromBody] ParmProduccionFiltro filtro)
        {
            PedidoAPL _apl = new PedidoAPL(configuration, configuracionAPP);
            return Consultar(() => _apl.Produccion(IdUsuario, filtro));
        }

        // Orden de producción (taller): joyero, material entregado, indicaciones y acabado
        [HttpPost]
        [Route("ordenproduccion")]
        public ActionResult OrdenProduccion([FromBody] ParmOrdenProduccion entidad)
        {
            PedidoAPL _apl = new PedidoAPL(configuration, configuracionAPP);
            return Procesar(() => _apl.OrdenProduccion(IdUsuario, entidad));
        }

        // Proforma u orden de producción: devuelve el número del documento
        [HttpPost]
        [Route("emitirdocumento")]
        public ActionResult EmitirDocumento([FromBody] ParmEmitirDocumento entidad)
        {
            PedidoAPL _apl = new PedidoAPL(configuration, configuracionAPP);
            return Procesar(() => _apl.EmitirDocumento(IdUsuario, entidad));
        }
    }
}
