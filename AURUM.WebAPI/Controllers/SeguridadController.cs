using AURUM.Aplicacion.Seguridad;
using AURUM.Modelos.Aplicacion;
using AURUM.Modelos.Seguridad;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace AURUM.WebAPI.Controllers
{
    // Usuarios e invitaciones de la empresa (las escrituras las valida la base: solo ADMIN)
    [Route("api/[controller]")]
    public class SeguridadController : ControladorBase
    {
        public SeguridadController(ILogger<SeguridadController> _logger, IConfiguration _configuration,
            IOptions<ConfiguracionAPP> _configuracionAPP) : base(_configuration, _configuracionAPP, _logger) { }

        [HttpGet]
        [Route("listarusuarios")]
        public ActionResult ListarUsuarios()
        {
            UsuarioAPL _apl = new UsuarioAPL(configuration, configuracionAPP);
            return Consultar(() => _apl.ListarUsuarios(IdUsuario));
        }

        [HttpPost]
        [Route("actualizarusuario")]
        public ActionResult ActualizarUsuario([FromBody] ParmUsuario entidad)
        {
            UsuarioAPL _apl = new UsuarioAPL(configuration, configuracionAPP);
            return Procesar(() => _apl.ActualizarUsuario(IdUsuario, entidad));
        }

        [HttpPost]
        [Route("asignarsucursales")]
        public ActionResult AsignarSucursales([FromBody] ParmUsuarioSucursales entidad)
        {
            UsuarioAPL _apl = new UsuarioAPL(configuration, configuracionAPP);
            return Procesar(() => _apl.AsignarSucursales(IdUsuario, entidad));
        }

        [HttpGet]
        [Route("listarinvitaciones")]
        public ActionResult ListarInvitaciones()
        {
            UsuarioAPL _apl = new UsuarioAPL(configuration, configuracionAPP);
            return Consultar(() => _apl.ListarInvitaciones(IdUsuario));
        }

        [HttpPost]
        [Route("invitar")]
        public ActionResult Invitar([FromBody] ParmInvitacion entidad)
        {
            UsuarioAPL _apl = new UsuarioAPL(configuration, configuracionAPP);
            return Procesar(() => _apl.Invitar(IdUsuario, entidad));
        }

        [HttpPost]
        [Route("reenviarinvitacion/{idinvitacion}")]
        public ActionResult ReenviarInvitacion(Guid idinvitacion)
        {
            UsuarioAPL _apl = new UsuarioAPL(configuration, configuracionAPP);
            return Procesar(() => _apl.ReenviarInvitacion(IdUsuario, idinvitacion));
        }

        [HttpPost]
        [Route("revocarinvitacion/{idinvitacion}")]
        public ActionResult RevocarInvitacion(Guid idinvitacion)
        {
            UsuarioAPL _apl = new UsuarioAPL(configuration, configuracionAPP);
            return Procesar(() => _apl.RevocarInvitacion(IdUsuario, idinvitacion));
        }
    }
}
