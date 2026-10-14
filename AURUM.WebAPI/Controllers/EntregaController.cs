using AURUM.Aplicacion.Principal;
using AURUM.Aplicacion.Ventas;
using AURUM.Modelos.Aplicacion;
using AURUM.Modelos.Ventas;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace AURUM.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class EntregaController : ControladorBase
    {
        public EntregaController(ILogger<EntregaController> _logger, IConfiguration _configuration,
            IOptions<ConfiguracionAPP> _configuracionAPP) : base(_configuration, _configuracionAPP, _logger) { }

        [HttpPost]
        [Route("listarentregas")]
        public ActionResult ListarEntregas([FromBody] ParmEntregaFiltro filtro)
        {
            PagoAPL _apl = new PagoAPL(configuration, configuracionAPP);
            return Consultar(() => _apl.ListarEntregas(IdUsuario, filtro));
        }

        // Programa o actualiza la entrega; ENTREGADA marca el pedido como entregado
        [HttpPost]
        [Route("entregaprc")]
        public ActionResult EntregaPrc([FromBody] ParmEntrega entidad)
        {
            PagoAPL _apl = new PagoAPL(configuration, configuracionAPP);
            return Procesar(() => _apl.EntregaPrc(IdUsuario, entidad));
        }

        // Firma del cliente (imagen PNG/JPG, multipart/form-data campo "archivo")
        [HttpPost]
        [Route("firma/{identrega}")]
        [RequestSizeLimit(20 * 1024 * 1024)]
        public ActionResult Firma(Guid identrega, IFormFile archivo)
        {
            ArchivoAPL _apl = new ArchivoAPL(configuration, configuracionAPP);
            return Procesar(() =>
            {
                using var contenido = archivo.OpenReadStream();
                return _apl.SubirFirma(IdUsuario, identrega, archivo.ContentType, archivo.Length, contenido);
            });
        }
    }
}
