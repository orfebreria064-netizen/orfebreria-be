using AURUM.Aplicacion.Almacen;
using AURUM.Modelos.Aplicacion;
using AURUM.Modelos.Materiales;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace AURUM.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class MaterialController : ControladorBase
    {
        public MaterialController(ILogger<MaterialController> _logger, IConfiguration _configuration,
            IOptions<ConfiguracionAPP> _configuracionAPP) : base(_configuration, _configuracionAPP, _logger) { }

        [HttpPost]
        [Route("listarmateriales")]
        public ActionResult ListarMateriales([FromBody] ParmMaterialFiltro filtro)
        {
            MaterialAPL _apl = new MaterialAPL(configuration, configuracionAPP);
            return Consultar(() => _apl.Listar(IdUsuario, filtro));
        }

        [HttpGet]
        [Route("stocksucursales/{idmaterial}")]
        public ActionResult StockSucursales(Guid idmaterial)
        {
            MaterialAPL _apl = new MaterialAPL(configuration, configuracionAPP);
            return Consultar(() => _apl.StockSucursales(IdUsuario, idmaterial));
        }

        [HttpPost]
        [Route("materialprc")]
        public ActionResult MaterialPrc([FromBody] ParmMaterial entidad)
        {
            MaterialAPL _apl = new MaterialAPL(configuration, configuracionAPP);
            return Procesar(() => _apl.MaterialPrc(IdUsuario, entidad));
        }

        [HttpPost]
        [Route("registrarmovimiento")]
        public ActionResult RegistrarMovimiento([FromBody] ParmMovimiento entidad)
        {
            MaterialAPL _apl = new MaterialAPL(configuration, configuracionAPP);
            return Procesar(() => _apl.RegistrarMovimiento(IdUsuario, entidad));
        }

        [HttpPost]
        [Route("trasladar")]
        public ActionResult Trasladar([FromBody] ParmTraslado entidad)
        {
            MaterialAPL _apl = new MaterialAPL(configuration, configuracionAPP);
            return Procesar(() => _apl.Trasladar(IdUsuario, entidad));
        }

        [HttpPost]
        [Route("stockminimo")]
        public ActionResult StockMinimo([FromBody] ParmStockMinimo entidad)
        {
            MaterialAPL _apl = new MaterialAPL(configuration, configuracionAPP);
            return Procesar(() => _apl.StockMinimo(IdUsuario, entidad));
        }

        [HttpPost]
        [Route("kardex")]
        public ActionResult Kardex([FromBody] ParmKardex filtro)
        {
            MaterialAPL _apl = new MaterialAPL(configuration, configuracionAPP);
            return Consultar(() => _apl.Kardex(IdUsuario, filtro));
        }

        [HttpGet]
        [Route("materialcliente/{idpedido}")]
        public ActionResult MaterialCliente(Guid idpedido)
        {
            MaterialAPL _apl = new MaterialAPL(configuration, configuracionAPP);
            return Consultar(() => _apl.MaterialCliente(IdUsuario, idpedido));
        }

        [HttpGet]
        [Route("preciosvigentes")]
        public ActionResult PreciosVigentes()
        {
            MaterialAPL _apl = new MaterialAPL(configuration, configuracionAPP);
            return Consultar(() => _apl.PreciosVigentes(IdUsuario));
        }

        [HttpPost]
        [Route("registrarprecios")]
        public ActionResult RegistrarPrecios([FromBody] ParmPrecios entidad)
        {
            MaterialAPL _apl = new MaterialAPL(configuration, configuracionAPP);
            return Procesar(() => _apl.RegistrarPrecios(IdUsuario, entidad));
        }

        [HttpGet]
        [Route("historialprecios/{idmaterial}")]
        public ActionResult HistorialPrecios(Guid idmaterial)
        {
            MaterialAPL _apl = new MaterialAPL(configuration, configuracionAPP);
            return Consultar(() => _apl.HistorialPrecios(IdUsuario, idmaterial));
        }
    }
}
