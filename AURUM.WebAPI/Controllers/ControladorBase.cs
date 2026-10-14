using AURUM.Configuracion;
using AURUM.Modelos.Aplicacion;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace AURUM.WebAPI.Controllers
{
    // Base de los controladores: todos exigen JWT salvo las acciones marcadas [AllowAnonymous].
    // El usuario sale siempre del token (nunca del body): los procedimientos validan con él
    // empresa, rol y sucursal.
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public abstract class ControladorBase : ControllerBase
    {
        protected readonly IConfiguration configuration;
        protected readonly IOptions<ConfiguracionAPP> configuracionAPP;
        protected readonly ILogger logger;

        protected ControladorBase(IConfiguration _configuration, IOptions<ConfiguracionAPP> _configuracionAPP, ILogger _logger)
        {
            configuration = _configuration;
            configuracionAPP = _configuracionAPP;
            logger = _logger;
        }

        protected Guid IdUsuario =>
            Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var id) ? id : Guid.Empty;

        protected string EmailUsuario => User.FindFirstValue(ClaimTypes.Email) ?? "";

        // Lecturas: 200 con los datos; los errores de negocio de la base pasan a 401/403/404/400.
        protected ActionResult Consultar(Func<Object?> consulta)
        {
            try
            {
                var respuesta = consulta();
                return respuesta == null ? NotFound(new { mensaje = "Registro no encontrado o sin permisos." }) : Ok(respuesta);
            }
            catch (SqlException ex) when (CodigosError.EsDeNegocio(ex.Number))
            {
                return StatusCode(CodigosError.EstadoHttp(ex.Number), new { mensaje = ex.Message, errorNumero = ex.Number });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error en {Accion}", ControllerContext.ActionDescriptor.DisplayName);
                return StatusCode(500, new { mensaje = "Ocurrió un error interno. Intente nuevamente." });
            }
        }

        // Escrituras: 200 con RespuestaProceso (ProcesoSwitch 0 = OK, 1 = validación, -1 = error interno), como en ARGO.
        // Una sesión no válida responde 401 para que el frontend cierre la sesión.
        protected ActionResult Procesar(Func<RespuestaProceso> proceso)
        {
            try
            {
                var respuesta = proceso();
                if (respuesta.ProcesoSwitch == -1)
                {
                    logger.LogError("Error de BD en {Accion}: {Numero} {Mensaje} ({Procedimiento} línea {Linea})",
                        ControllerContext.ActionDescriptor.DisplayName, respuesta.ErrorNumero, respuesta.Mensaje,
                        respuesta.ErrorProcedimiento, respuesta.ErrorLinea);
                    respuesta.Mensaje = "Ocurrió un error interno al procesar la solicitud.";
                }
                if (respuesta.ProcesoSwitch == 1 && respuesta.ErrorNumero == CodigosError.SesionNoValida)
                    return Unauthorized(respuesta);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error en {Accion}", ControllerContext.ActionDescriptor.DisplayName);
                return StatusCode(500, new RespuestaProceso { ProcesoSwitch = -1, Mensaje = "Ocurrió un error interno al procesar la solicitud." });
            }
        }
    }
}
