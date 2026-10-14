using AURUM.Aplicacion.Ventas;
using AURUM.Modelos.Aplicacion;
using AURUM.Modelos.Clientes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace AURUM.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class ClienteController : ControladorBase
    {
        public ClienteController(ILogger<ClienteController> _logger, IConfiguration _configuration,
            IOptions<ConfiguracionAPP> _configuracionAPP) : base(_configuration, _configuracionAPP, _logger) { }

        [HttpPost]
        [Route("listarclientes")]
        public ActionResult ListarClientes([FromBody] ParmClienteFiltro filtro)
        {
            ClienteAPL _apl = new ClienteAPL(configuration, configuracionAPP);
            return Consultar(() => _apl.Listar(IdUsuario, filtro));
        }

        [HttpGet]
        [Route("traeruno/{idcliente}")]
        public ActionResult TraerUno(Guid idcliente)
        {
            ClienteAPL _apl = new ClienteAPL(configuration, configuracionAPP);
            return Consultar(() => _apl.TraerUno(IdUsuario, idcliente));
        }

        [HttpPost]
        [Route("clienteprc")]
        public ActionResult ClientePrc([FromBody] ParmCliente entidad)
        {
            ClienteAPL _apl = new ClienteAPL(configuration, configuracionAPP);
            return Procesar(() => _apl.ClientePrc(IdUsuario, entidad));
        }

        [HttpPost]
        [Route("contactoprc")]
        public ActionResult ContactoPrc([FromBody] ParmContacto entidad)
        {
            ClienteAPL _apl = new ClienteAPL(configuration, configuracionAPP);
            return Procesar(() => _apl.ContactoPrc(IdUsuario, entidad));
        }

        [HttpPost]
        [Route("eliminarcontacto/{idcontacto}")]
        public ActionResult EliminarContacto(Guid idcontacto)
        {
            ClienteAPL _apl = new ClienteAPL(configuration, configuracionAPP);
            return Procesar(() => _apl.EliminarContacto(IdUsuario, idcontacto));
        }
    }
}
