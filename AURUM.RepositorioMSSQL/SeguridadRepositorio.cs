using AURUM.Modelos.Aplicacion;
using AURUM.Modelos.Seguridad;
using Dapper;
using System.Text.Json;

namespace AURUM.RepositorioMSSQL;

public class SeguridadRepositorio : RepositorioMSSQL
{
    public SeguridadRepositorio(string cadenaConexionBD) : base(cadenaConexionBD) { }

    // ---------- Autenticación ----------
    public UsuarioLogin? TraerLogin(string email)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@email", email);
        return base.TraerUno<UsuarioLogin>("api.usp_auth_get_login", parametros);
    }

    public RespuestaProceso RegistrarIntentoLogin(Guid idUsuario, bool exito)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@user_id", idUsuario);
        parametros.Add("@success", exito);
        return base.Procesar("api.usp_auth_login_result", parametros);
    }

    public RespuestaProceso CrearPrimerUsuario(ParmPrimerUsuario entidad, string claveHash)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@email", entidad.email);
        parametros.Add("@full_name", entidad.full_name);
        parametros.Add("@password_hash", claveHash);
        return base.Procesar("api.usp_auth_setup_first_user", parametros);
    }

    public Object? TraerInvitacion(string token)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@token", token);
        return base.TraerUno("api.usp_auth_get_invitation", parametros);
    }

    public RespuestaProceso AceptarInvitacion(ParmAceptarInvitacion entidad, string claveHash)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@token", entidad.token);
        parametros.Add("@password_hash", claveHash);
        parametros.Add("@full_name", entidad.full_name);
        return base.Procesar("api.usp_auth_accept_invitation", parametros);
    }

    public RespuestaProceso SolicitarRecuperacion(string email)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@email", email);
        return base.Procesar("api.usp_auth_request_password_reset", parametros);
    }

    public RespuestaProceso RestablecerClave(string token, string claveHash)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@token", token);
        parametros.Add("@password_hash", claveHash);
        return base.Procesar("api.usp_auth_reset_password", parametros);
    }

    public RespuestaProceso CambiarClave(Guid idUsuario, string claveHash)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@user_id", idUsuario);
        parametros.Add("@password_hash", claveHash);
        return base.Procesar("api.usp_auth_change_password", parametros);
    }

    // ---------- Sesión y perfil ----------
    public Object TraerSesion(Guid idUsuario)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@user_id", idUsuario);
        return base.ListarMultiple("api.usp_me_get", parametros, new[] { "perfil", "suscripcion", "sucursales" }, 2);
    }

    public RespuestaProceso ActualizarPerfil(Guid idUsuario, ParmPerfil entidad)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@user_id", idUsuario);
        parametros.Add("@full_name", entidad.full_name);
        parametros.Add("@phone", entidad.phone);
        parametros.Add("@default_branch_id", entidad.default_branch_id);
        return base.Procesar("api.usp_profile_update_self", parametros);
    }

    // ---------- Usuarios e invitaciones ----------
    public Object ListarUsuarios(Guid idUsuario)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@user_id", idUsuario);
        return base.Listar("api.usp_users_list", parametros);
    }

    public RespuestaProceso ActualizarUsuario(Guid idUsuario, ParmUsuario entidad)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@user_id", idUsuario);
        parametros.Add("@target_user_id", entidad.user_id);
        parametros.Add("@role", entidad.role);
        parametros.Add("@active", entidad.active);
        parametros.Add("@full_name", entidad.full_name);
        return base.Procesar("api.usp_user_update", parametros);
    }

    public RespuestaProceso AsignarSucursales(Guid idUsuario, ParmUsuarioSucursales entidad)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@user_id", idUsuario);
        parametros.Add("@target_user_id", entidad.user_id);
        parametros.Add("@branch_ids", JsonSerializer.Serialize(entidad.branch_ids));
        return base.Procesar("api.usp_user_set_branches", parametros);
    }

    public RespuestaProceso CrearInvitacion(Guid idUsuario, ParmInvitacion entidad)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@user_id", idUsuario);
        parametros.Add("@email", entidad.email);
        parametros.Add("@full_name", entidad.full_name);
        parametros.Add("@role", entidad.role);
        parametros.Add("@branch_ids", entidad.branch_ids == null ? null : JsonSerializer.Serialize(entidad.branch_ids));
        return base.Procesar("api.usp_invitation_create", parametros);
    }

    public RespuestaProceso RenovarInvitacion(Guid idUsuario, Guid idInvitacion)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@user_id", idUsuario);
        parametros.Add("@invitation_id", idInvitacion);
        return base.Procesar("api.usp_invitation_renew", parametros);
    }

    public RespuestaProceso RevocarInvitacion(Guid idUsuario, Guid idInvitacion)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@user_id", idUsuario);
        parametros.Add("@invitation_id", idInvitacion);
        return base.Procesar("api.usp_invitation_revoke", parametros);
    }

    public Object ListarInvitaciones(Guid idUsuario)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@user_id", idUsuario);
        return base.Listar("api.usp_invitations_list", parametros);
    }
}
