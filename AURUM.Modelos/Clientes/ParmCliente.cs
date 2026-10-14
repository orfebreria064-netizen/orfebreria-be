namespace AURUM.Modelos.Clientes
{
    public class ParmCliente
    {
        public Guid? id { get; set; }
        public string person_type { get; set; } = "NATURAL";
        public string? first_name { get; set; }
        public string? last_name { get; set; }
        public string? business_name { get; set; }
        public string document_type { get; set; } = "DNI";
        public string? document_number { get; set; }
        public string? phone { get; set; }
        public string? whatsapp { get; set; }
        public string? email { get; set; }
        public string? address { get; set; }
        public string? city { get; set; }
        public string? notes { get; set; }
        public bool active { get; set; } = true;
    }

    public class ParmClienteFiltro
    {
        public string? search { get; set; }
        public bool include_inactive { get; set; }
    }

    public class ParmContacto
    {
        public Guid? id { get; set; }
        public Guid customer_id { get; set; }
        public string name { get; set; } = "";
        public string? position { get; set; }
        public string? phone { get; set; }
        public string? email { get; set; }
        public string? notes { get; set; }
    }
}
