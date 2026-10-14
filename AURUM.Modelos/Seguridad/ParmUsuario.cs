namespace AURUM.Modelos.Seguridad
{
    public class ParmPerfil
    {
        public string full_name { get; set; } = "";
        public string? phone { get; set; }
        public Guid? default_branch_id { get; set; }
    }

    public class ParmUsuario
    {
        public Guid user_id { get; set; }
        public string role { get; set; } = "";
        public bool active { get; set; }
    }

    public class ParmUsuarioSucursales
    {
        public Guid user_id { get; set; }
        public List<Guid> branch_ids { get; set; } = new();
    }

    public class ParmInvitacion
    {
        public string email { get; set; } = "";
        public string? full_name { get; set; }
        public string role { get; set; } = "";
        public List<Guid>? branch_ids { get; set; }        // null = sucursal principal
    }
}
