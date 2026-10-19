using AURUM.Aplicacion.Base;
using AURUM.Modelos.Aplicacion;
using AURUM.Modelos.Seguridad;
using AURUM.Notifica;
using AURUM.RepositorioMSSQL;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace AURUM.Aplicacion.Seguridad;

public class SeguridadAPL : AplicacionBase
{
    const string CredencialesIncorrectas = "Correo o contraseña incorrectos.";

    public SeguridadAPL(IConfiguration iConfiguration, IOptions<ConfiguracionAPP> iConfiguracionAPP) : base(iConfiguration, iConfiguracionAPP)
    { }

    // Valida credenciales. El token lo genera el controlador (IAutenticaService) si estado = 1.
    public RespuestaLogin Valida(UserParm userParm)
    {
        SeguridadRepositorio _repositorio = new SeguridadRepositorio(parametrosApp.BD);
        var usuario = _repositorio.TraerLogin(userParm.email);

        if (usuario == null || string.IsNullOrEmpty(usuario.password_hash))
            return new RespuestaLogin { estado = 0, mensaje = CredencialesIncorrectas };
        if (usuario.locked_until > DateTimeOffset.UtcNow)
            return new RespuestaLogin { estado = 0, mensaje = "Demasiados intentos fallidos. Intente nuevamente en unos minutos." };

        bool claveCorrecta = ClaveAPL.Verificar(usuario.password_hash, userParm.clave);
        _repositorio.RegistrarIntentoLogin(usuario.user_id, claveCorrecta);
        if (!claveCorrecta)
            return new RespuestaLogin { estado = 0, mensaje = CredencialesIncorrectas };
        if (usuario.active != true && !usuario.is_platform_admin)
            return new RespuestaLogin { estado = 0, mensaje = "Su usuario está inactivo. Contacte al administrador de su empresa." };

        return new RespuestaLogin
        {
            estado = 1,
            mensaje = "Bienvenido.",
            user_id = usuario.user_id,
            full_name = usuario.full_name,
            email = usuario.email,
            role = usuario.role,
            org_id = usuario.org_id,
            org_state = usuario.org_state,
            is_platform_admin = usuario.is_platform_admin
        };
    }

    // Instalación vacía: el primer usuario es el superadministrador (sin empresa)
    public RespuestaProceso PrimerUsuario(ParmPrimerUsuario entidad)
    {
        var error = ClaveAPL.Validar(entidad.clave);
        if (error != null) return new RespuestaProceso { ProcesoSwitch = 1, Mensaje = error };

        SeguridadRepositorio _repositorio = new SeguridadRepositorio(parametrosApp.BD);
        return _repositorio.CrearPrimerUsuario(entidad, ClaveAPL.Generar(entidad.clave));
    }

    public Object? TraerInvitacion(string token)
    {
        SeguridadRepositorio _repositorio = new SeguridadRepositorio(parametrosApp.BD);
        return _repositorio.TraerInvitacion(token);
    }

    public RespuestaProceso AceptarInvitacion(ParmAceptarInvitacion entidad)
    {
        var error = ClaveAPL.Validar(entidad.clave);
        if (error != null) return new RespuestaProceso { ProcesoSwitch = 1, Mensaje = error };

        SeguridadRepositorio _repositorio = new SeguridadRepositorio(parametrosApp.BD);
        return _repositorio.AceptarInvitacion(entidad, ClaveAPL.Generar(entidad.clave));
    }

    // Siempre responde lo mismo, exista o no el correo, para no revelar qué cuentas existen.
    public RespuestaProceso RecuperarClave(ParmRecuperarClave entidad)
    {
        SeguridadRepositorio _repositorio = new SeguridadRepositorio(parametrosApp.BD);
        var respuesta = _repositorio.SolicitarRecuperacion(entidad.email);
        var token = Valor(respuesta.Data, "token");
        if (respuesta.ProcesoSwitch == 0 && token != null)
        {
            Correo().enviarCorreo(new MensajeNotificacion
            {
                emailDestino = Valor(respuesta.Data, "email")!,
                asunto = "Restablecer contraseña - Orfebrería Aurum",
                html_email_body = PlantillaCorreo.RecuperarClave(Valor(respuesta.Data, "full_name"),
                                    EnlaceFrontend($"activar-cuenta?tipo=recuperacion&token={token}"))
            });
        }
        return new RespuestaProceso
        {
            ProcesoSwitch = 0,
            Mensaje = "Si el correo está registrado, le enviamos un enlace para restablecer su contraseña."
        };
    }

    public RespuestaProceso RestablecerClave(ParmRestablecerClave entidad)
    {
        var error = ClaveAPL.Validar(entidad.clave);
        if (error != null) return new RespuestaProceso { ProcesoSwitch = 1, Mensaje = error };

        SeguridadRepositorio _repositorio = new SeguridadRepositorio(parametrosApp.BD);
        return _repositorio.RestablecerClave(entidad.token, ClaveAPL.Generar(entidad.clave));
    }

    public RespuestaProceso CambiarClave(Guid idUsuario, string email, ParmCambioClave entidad)
    {
        var error = ClaveAPL.Validar(entidad.clave_nueva);
        if (error != null) return new RespuestaProceso { ProcesoSwitch = 1, Mensaje = error };

        SeguridadRepositorio _repositorio = new SeguridadRepositorio(parametrosApp.BD);
        var usuario = _repositorio.TraerLogin(email);
        if (usuario == null || usuario.user_id != idUsuario || string.IsNullOrEmpty(usuario.password_hash)
            || !ClaveAPL.Verificar(usuario.password_hash, entidad.clave_actual))
            return new RespuestaProceso { ProcesoSwitch = 1, Mensaje = "La contraseña actual no es correcta." };

        return _repositorio.CambiarClave(idUsuario, ClaveAPL.Generar(entidad.clave_nueva));
    }

    public Object TraerSesion(Guid idUsuario)
    {
        SeguridadRepositorio _repositorio = new SeguridadRepositorio(parametrosApp.BD);
        return _repositorio.TraerSesion(idUsuario);
    }

    public RespuestaProceso ActualizarPerfil(Guid idUsuario, ParmPerfil entidad)
    {
        SeguridadRepositorio _repositorio = new SeguridadRepositorio(parametrosApp.BD);
        return _repositorio.ActualizarPerfil(idUsuario, entidad);
    }
}
