using AURUM.Aplicacion.Seguridad;
using AURUM.Modelos.Aplicacion;
using AURUM.Modelos.Seguridad;
using AURUM.WebAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.Options;

namespace AURUM.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class LoginController : ControladorBase
    {
        private readonly IAutenticaService _authenticationService;

        public LoginController(ILogger<LoginController> _logger,
            IConfiguration _configuration,
            IOptions<ConfiguracionAPP> _configuracionAPP,
            IAutenticaService authenticationService) : base(_configuration, _configuracionAPP, _logger)
        {
            _authenticationService = authenticationService; //Servicio de Autenticacion Token
        }

        [AllowAnonymous]
        [EnableRateLimiting("publico")]
        [HttpPost]
        [Route("validausuario")]
        [Produces("application/json", Type = typeof(RespuestaLogin))]
        public ActionResult ValidaUsuario([FromBody] UserParm dataFromBody)
        {
            try
            {
                SeguridadAPL loginAPL = new SeguridadAPL(configuration, configuracionAPP);
                var respuestaLogin = loginAPL.Valida(dataFromBody);
                if (respuestaLogin.estado == 1)
                    respuestaLogin.token = _authenticationService.Autentica(respuestaLogin);
                return Ok(respuestaLogin);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error en login");
                return StatusCode(500, new RespuestaLogin { estado = 0, mensaje = "No se pudo iniciar sesión. Intente nuevamente." });
            }
        }

        // Instalación vacía: crea el superadministrador de la plataforma (sin empresa)
        [AllowAnonymous]
        [EnableRateLimiting("publico")]
        [HttpPost]
        [Route("primerusuario")]
        public ActionResult PrimerUsuario([FromBody] ParmPrimerUsuario dataFromBody)
        {
            SeguridadAPL _apl = new SeguridadAPL(configuration, configuracionAPP);
            return Procesar(() => _apl.PrimerUsuario(dataFromBody));
        }

        // Datos de la invitación para la pantalla "Crear contraseña"
        [AllowAnonymous]
        [EnableRateLimiting("publico")]
        [HttpGet]
        [Route("invitacion/{token}")]
        public ActionResult TraerInvitacion(string token)
        {
            SeguridadAPL _apl = new SeguridadAPL(configuration, configuracionAPP);
            return Consultar(() => _apl.TraerInvitacion(token));
        }

        [AllowAnonymous]
        [EnableRateLimiting("publico")]
        [HttpPost]
        [Route("aceptarinvitacion")]
        public ActionResult AceptarInvitacion([FromBody] ParmAceptarInvitacion dataFromBody)
        {
            SeguridadAPL _apl = new SeguridadAPL(configuration, configuracionAPP);
            return Procesar(() => _apl.AceptarInvitacion(dataFromBody));
        }

        [AllowAnonymous]
        [EnableRateLimiting("publico")]
        [HttpPost]
        [Route("recuperarclave")]
        public ActionResult RecuperarClave([FromBody] ParmRecuperarClave dataFromBody)
        {
            SeguridadAPL _apl = new SeguridadAPL(configuration, configuracionAPP);
            return Procesar(() => _apl.RecuperarClave(dataFromBody));
        }

        [AllowAnonymous]
        [EnableRateLimiting("publico")]
        [HttpPost]
        [Route("restablecerclave")]
        public ActionResult RestablecerClave([FromBody] ParmRestablecerClave dataFromBody)
        {
            SeguridadAPL _apl = new SeguridadAPL(configuration, configuracionAPP);
            return Procesar(() => _apl.RestablecerClave(dataFromBody));
        }

        [HttpPost]
        [Route("cambiarclave")]
        public ActionResult CambiarClave([FromBody] ParmCambioClave dataFromBody)
        {
            SeguridadAPL _apl = new SeguridadAPL(configuration, configuracionAPP);
            return Procesar(() => _apl.CambiarClave(IdUsuario, EmailUsuario, dataFromBody));
        }

        // Perfil, plan y sucursales del usuario (funciona aun con la empresa suspendida)
        [HttpGet]
        [Route("sesion")]
        public ActionResult Sesion()
        {
            SeguridadAPL _apl = new SeguridadAPL(configuration, configuracionAPP);
            return Consultar(() => _apl.TraerSesion(IdUsuario));
        }

        [HttpPost]
        [Route("actualizarperfil")]
        public ActionResult ActualizarPerfil([FromBody] ParmPerfil dataFromBody)
        {
            SeguridadAPL _apl = new SeguridadAPL(configuration, configuracionAPP);
            return Procesar(() => _apl.ActualizarPerfil(IdUsuario, dataFromBody));
        }
    }
}
