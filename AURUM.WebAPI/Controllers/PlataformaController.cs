using AURUM.Aplicacion.Plataforma;
using AURUM.Modelos.Aplicacion;
using AURUM.Modelos.Plataforma;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.Options;

namespace AURUM.WebAPI.Controllers
{
    // Plataforma SaaS. Salvo planes y solicitud de acceso, todo es del superadministrador
    // (la base lo valida con platform_admins).
    [Route("api/[controller]")]
    public class PlataformaController : ControladorBase
    {
        public PlataformaController(ILogger<PlataformaController> _logger, IConfiguration _configuration,
            IOptions<ConfiguracionAPP> _configuracionAPP) : base(_configuration, _configuracionAPP, _logger) { }

        [AllowAnonymous]
        [HttpGet]
        [Route("planes")]
        public ActionResult Planes()
        {
            PlataformaAPL _apl = new PlataformaAPL(configuration, configuracionAPP);
            return Consultar(() => _apl.ListarPlanes(false));
        }

        [AllowAnonymous]
        [EnableRateLimiting("publico")]
        [HttpPost]
        [Route("solicitaracceso")]
        public ActionResult SolicitarAcceso([FromBody] ParmSolicitudAcceso entidad)
        {
            PlataformaAPL _apl = new PlataformaAPL(configuration, configuracionAPP);
            return Procesar(() => _apl.SolicitarAcceso(entidad));
        }

        [HttpGet]
        [Route("todosplanes")]
        public ActionResult TodosPlanes()
        {
            PlataformaAPL _apl = new PlataformaAPL(configuration, configuracionAPP);
            return Consultar(() => _apl.ListarPlanes(true));
        }

        [HttpPost]
        [Route("actualizarplan")]
        public ActionResult ActualizarPlan([FromBody] ParmPlan entidad)
        {
            PlataformaAPL _apl = new PlataformaAPL(configuration, configuracionAPP);
            return Procesar(() => _apl.ActualizarPlan(IdUsuario, entidad));
        }

        [HttpGet]
        [Route("solicitudes")]
        public ActionResult Solicitudes([FromQuery] string? estado)
        {
            PlataformaAPL _apl = new PlataformaAPL(configuration, configuracionAPP);
            return Consultar(() => _apl.ListarSolicitudes(IdUsuario, estado));
        }

        [HttpPost]
        [Route("aprobarsolicitud")]
        public ActionResult AprobarSolicitud([FromBody] ParmAprobarSolicitud entidad)
        {
            PlataformaAPL _apl = new PlataformaAPL(configuration, configuracionAPP);
            return Procesar(() => _apl.AprobarSolicitud(IdUsuario, entidad));
        }

        [HttpPost]
        [Route("rechazarsolicitud")]
        public ActionResult RechazarSolicitud([FromBody] ParmRechazarSolicitud entidad)
        {
            PlataformaAPL _apl = new PlataformaAPL(configuration, configuracionAPP);
            return Procesar(() => _apl.RechazarSolicitud(IdUsuario, entidad));
        }

        [HttpPost]
        [Route("invitaradmin")]
        public ActionResult InvitarAdmin([FromBody] ParmInvitarAdmin entidad)
        {
            PlataformaAPL _apl = new PlataformaAPL(configuration, configuracionAPP);
            return Procesar(() => _apl.InvitarAdmin(IdUsuario, entidad));
        }

        [HttpPost]
        [Route("registrarpago")]
        public ActionResult RegistrarPago([FromBody] ParmPagoSuscripcion entidad)
        {
            PlataformaAPL _apl = new PlataformaAPL(configuration, configuracionAPP);
            return Procesar(() => _apl.RegistrarPago(IdUsuario, entidad));
        }

        [HttpPost]
        [Route("actualizarsuscripcion")]
        public ActionResult ActualizarSuscripcion([FromBody] ParmSuscripcion entidad)
        {
            PlataformaAPL _apl = new PlataformaAPL(configuration, configuracionAPP);
            return Procesar(() => _apl.ActualizarSuscripcion(IdUsuario, entidad));
        }

        [HttpGet]
        [Route("empresas")]
        public ActionResult Empresas()
        {
            PlataformaAPL _apl = new PlataformaAPL(configuration, configuracionAPP);
            return Consultar(() => _apl.ListarEmpresas(IdUsuario));
        }

        [HttpGet]
        [Route("resumen")]
        public ActionResult Resumen()
        {
            PlataformaAPL _apl = new PlataformaAPL(configuration, configuracionAPP);
            return Consultar(() => _apl.Resumen(IdUsuario));
        }

        // Superadmin: ?idempresa= opcional. ADMIN de empresa: sus propios pagos.
        [HttpGet]
        [Route("pagossuscripcion")]
        public ActionResult PagosSuscripcion([FromQuery] Guid? idempresa)
        {
            PlataformaAPL _apl = new PlataformaAPL(configuration, configuracionAPP);
            return Consultar(() => _apl.PagosSuscripcion(IdUsuario, idempresa));
        }
    }
}
