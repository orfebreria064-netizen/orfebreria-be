namespace AURUM.Modelos.Seguridad
{
    // Credenciales del login
    public class UserParm
    {
        public string email { get; set; } = "";
        public string clave { get; set; } = "";
    }

    public class RespuestaLogin
    {
        public Int16 estado { get; set; }                 // 1: OK ; 0: credenciales no válidas o cuenta bloqueada
        public string? mensaje { get; set; }
        public string? token { get; set; }
        public Guid? user_id { get; set; }
        public string? full_name { get; set; }
        public string? email { get; set; }
        public string? role { get; set; }
        public Guid? org_id { get; set; }
        public string? org_state { get; set; }            // TRIAL | ACTIVE | GRACE | EXPIRED | SUSPENDED | CANCELLED
        public bool is_platform_admin { get; set; }
    }

    // Fila de api.usp_auth_get_login
    public class UsuarioLogin
    {
        public Guid user_id { get; set; }
        public string email { get; set; } = "";
        public string? password_hash { get; set; }
        public DateTimeOffset? locked_until { get; set; }
        public string? full_name { get; set; }
        public string? role { get; set; }
        public bool? active { get; set; }
        public Guid? org_id { get; set; }
        public string? org_state { get; set; }
        public bool is_platform_admin { get; set; }
    }

    public class ParmPrimerUsuario
    {
        public string email { get; set; } = "";
        public string full_name { get; set; } = "";
        public string clave { get; set; } = "";
        public string? company_name { get; set; }
    }

    public class ParmAceptarInvitacion
    {
        public string token { get; set; } = "";
        public string clave { get; set; } = "";
        public string? full_name { get; set; }
    }

    public class ParmRecuperarClave
    {
        public string email { get; set; } = "";
    }

    public class ParmRestablecerClave
    {
        public string token { get; set; } = "";
        public string clave { get; set; } = "";
    }

    public class ParmCambioClave
    {
        public string clave_actual { get; set; } = "";
        public string clave_nueva { get; set; } = "";
    }
}
