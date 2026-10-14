using AURUM.Aplicacion.Configuracion;
using AURUM.Modelos.Aplicacion;
using AURUM.Modelos.Configuracion;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace AURUM.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class ConfiguracionController : ControladorBase
    {
        public ConfiguracionController(ILogger<ConfiguracionController> _logger, IConfiguration _configuration,
            IOptions<ConfiguracionAPP> _configuracionAPP) : base(_configuration, _configuracionAPP, _logger) { }

        [HttpGet]
        [Route("traerconfiguracion")]
        public ActionResult TraerConfiguracion()
        {
            ConfiguracionAPL _apl = new ConfiguracionAPL(configuration, configuracionAPP);
            return Consultar(() => _apl.TraerConfiguracion(IdUsuario));
        }

        [HttpPost]
        [Route("actualizarconfiguracion")]
        public ActionResult ActualizarConfiguracion([FromBody] ParmConfiguracion entidad)
        {
            ConfiguracionAPL _apl = new ConfiguracionAPL(configuration, configuracionAPP);
            return Procesar(() => _apl.ActualizarConfiguracion(IdUsuario, entidad));
        }

        [HttpGet]
        [Route("listarservicios")]
        public ActionResult ListarServicios([FromQuery] bool incluirinactivos = false)
        {
            ConfiguracionAPL _apl = new ConfiguracionAPL(configuration, configuracionAPP);
            return Consultar(() => _apl.ListarServicios(IdUsuario, incluirinactivos));
        }

        [HttpPost]
        [Route("servicioprc")]
        public ActionResult ServicioPrc([FromBody] ParmServicio entidad)
        {
            ConfiguracionAPL _apl = new ConfiguracionAPL(configuration, configuracionAPP);
            return Procesar(() => _apl.ServicioPrc(IdUsuario, entidad));
        }

        [HttpGet]
        [Route("listaretapas")]
        public ActionResult ListarEtapas([FromQuery] bool incluirinactivos = false)
        {
            ConfiguracionAPL _apl = new ConfiguracionAPL(configuration, configuracionAPP);
            return Consultar(() => _apl.ListarEtapas(IdUsuario, incluirinactivos));
        }

        [HttpPost]
        [Route("etapaprc")]
        public ActionResult EtapaPrc([FromBody] ParmEtapa entidad)
        {
            ConfiguracionAPL _apl = new ConfiguracionAPL(configuration, configuracionAPP);
            return Procesar(() => _apl.EtapaPrc(IdUsuario, entidad));
        }

        [HttpGet]
        [Route("listarsucursales")]
        public ActionResult ListarSucursales()
        {
            ConfiguracionAPL _apl = new ConfiguracionAPL(configuration, configuracionAPP);
            return Consultar(() => _apl.ListarSucursales(IdUsuario));
        }

        [HttpPost]
        [Route("sucursalprc")]
        public ActionResult SucursalPrc([FromBody] ParmSucursal entidad)
        {
            ConfiguracionAPL _apl = new ConfiguracionAPL(configuration, configuracionAPP);
            return Procesar(() => _apl.SucursalPrc(IdUsuario, entidad));
        }

        [HttpPost]
        [Route("sucursalprincipal/{idsucursal}")]
        public ActionResult SucursalPrincipal(Guid idsucursal)
        {
            ConfiguracionAPL _apl = new ConfiguracionAPL(configuration, configuracionAPP);
            return Procesar(() => _apl.SucursalPrincipal(IdUsuario, idsucursal));
        }
    }
}
