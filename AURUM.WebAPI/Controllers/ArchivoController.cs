using AURUM.Aplicacion.Principal;
using AURUM.Modelos.Aplicacion;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace AURUM.WebAPI.Controllers
{
    // Adjuntos de pedidos. Los archivos se sirven solo a quien puede ver el pedido.
    [Route("api/[controller]")]
    public class ArchivoController : ControladorBase
    {
        public ArchivoController(ILogger<ArchivoController> _logger, IConfiguration _configuration,
            IOptions<ConfiguracionAPP> _configuracionAPP) : base(_configuration, _configuracionAPP, _logger) { }

        // multipart/form-data: archivo, categoria (FOTO|DISENO|BOCETO|DOCUMENTO|EVIDENCIA), descripcion
        [HttpPost]
        [Route("subir/{idpedido}")]
        [RequestSizeLimit(20 * 1024 * 1024)]
        public ActionResult Subir(Guid idpedido, IFormFile archivo, [FromForm] string categoria = "FOTO", [FromForm] string? descripcion = null)
        {
            ArchivoAPL _apl = new ArchivoAPL(configuration, configuracionAPP);
            return Procesar(() =>
            {
                using var contenido = archivo.OpenReadStream();
                return _apl.Subir(IdUsuario, idpedido, categoria, archivo.FileName, archivo.ContentType, archivo.Length, contenido, descripcion);
            });
        }

        // ?ruta= storage_path del adjunto o signature_path de la entrega
        [HttpGet]
        [Route("descargar")]
        public ActionResult Descargar([FromQuery] string ruta)
        {
            try
            {
                ArchivoAPL _apl = new ArchivoAPL(configuration, configuracionAPP);
                var archivo = _apl.Descargar(IdUsuario, ruta);
                if (archivo == null) return NotFound(new { mensaje = "Archivo no encontrado o sin permisos." });
                return PhysicalFile(archivo.ruta_completa, archivo.tipo_mime, archivo.nombre);
            }
            catch (UnauthorizedAccessException)
            {
                return NotFound(new { mensaje = "Archivo no encontrado o sin permisos." });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al descargar {Ruta}", ruta);
                return StatusCode(500, new { mensaje = "No se pudo descargar el archivo." });
            }
        }

        [HttpPost]
        [Route("eliminar/{idarchivo}")]
        public ActionResult Eliminar(Guid idarchivo)
        {
            ArchivoAPL _apl = new ArchivoAPL(configuration, configuracionAPP);
            return Procesar(() => _apl.Eliminar(IdUsuario, idarchivo));
        }
    }
}
