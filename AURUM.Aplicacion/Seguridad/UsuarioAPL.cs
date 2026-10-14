using AURUM.Aplicacion.Base;
using AURUM.Modelos.Aplicacion;
using AURUM.Modelos.Seguridad;
using AURUM.Notifica;
using AURUM.RepositorioMSSQL;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace AURUM.Aplicacion.Seguridad;

// Usuarios e invitaciones de la empresa
public class UsuarioAPL : AplicacionBase
{
    public UsuarioAPL(IConfiguration iConfiguration, IOptions<ConfiguracionAPP> iConfiguracionAPP) : base(iConfiguration, iConfiguracionAPP)
    { }

    public Object ListarUsuarios(Guid idUsuario)
    {
        SeguridadRepositorio _repositorio = new SeguridadRepositorio(parametrosApp.BD);
        return _repositorio.ListarUsuarios(idUsuario);
    }

    public RespuestaProceso ActualizarUsuario(Guid idUsuario, ParmUsuario entidad)
    {
        SeguridadRepositorio _repositorio = new SeguridadRepositorio(parametrosApp.BD);
        return _repositorio.ActualizarUsuario(idUsuario, entidad);
    }

    public RespuestaProceso AsignarSucursales(Guid idUsuario, ParmUsuarioSucursales entidad)
    {
        SeguridadRepositorio _repositorio = new SeguridadRepositorio(parametrosApp.BD);
        return _repositorio.AsignarSucursales(idUsuario, entidad);
    }

    public RespuestaProceso Invitar(Guid idUsuario, ParmInvitacion entidad)
    {
        SeguridadRepositorio _repositorio = new SeguridadRepositorio(parametrosApp.BD);
        return EnviarInvitacion(_repositorio.CrearInvitacion(idUsuario, entidad));
    }

    public RespuestaProceso ReenviarInvitacion(Guid idUsuario, Guid idInvitacion)
    {
        SeguridadRepositorio _repositorio = new SeguridadRepositorio(parametrosApp.BD);
        return EnviarInvitacion(_repositorio.RenovarInvitacion(idUsuario, idInvitacion));
    }

    public RespuestaProceso RevocarInvitacion(Guid idUsuario, Guid idInvitacion)
    {
        SeguridadRepositorio _repositorio = new SeguridadRepositorio(parametrosApp.BD);
        return _repositorio.RevocarInvitacion(idUsuario, idInvitacion);
    }

    public Object ListarInvitaciones(Guid idUsuario)
    {
        SeguridadRepositorio _repositorio = new SeguridadRepositorio(parametrosApp.BD);
        return _repositorio.ListarInvitaciones(idUsuario);
    }

    // Envía el correo con el enlace y quita el token de la respuesta (solo viaja por correo).
    // También lo usa PlataformaAPL al aprobar solicitudes e invitar administradores.
    internal RespuestaProceso EnviarInvitacion(RespuestaProceso respuesta)
    {
        var token = Valor(respuesta.Data, "token");
        if (respuesta.ProcesoSwitch != 0 || token == null) return respuesta;

        bool enviado = Correo().enviarCorreo(new MensajeNotificacion
        {
            emailDestino = Valor(respuesta.Data, "email")!,
            asunto = $"Invitación a {Valor(respuesta.Data, "company_name")} - Orfebrería Aurum",
            html_email_body = PlantillaCorreo.Invitacion(Valor(respuesta.Data, "full_name"), Valor(respuesta.Data, "company_name") ?? "",
                                Valor(respuesta.Data, "role") ?? "ADMIN", EnlaceFrontend($"activar-cuenta?tipo=invitacion&token={token}"))
        });

        respuesta.Data = SinColumnas(respuesta.Data, "token");
        respuesta.Mensaje = enviado
            ? "Invitación enviada."
            : "Invitación creada, pero no se pudo enviar el correo. Use «Reenviar» más tarde.";
        return respuesta;
    }
}
