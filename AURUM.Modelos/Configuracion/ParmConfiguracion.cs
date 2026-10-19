namespace AURUM.Modelos.Configuracion
{
    // Datos de la empresa (settings)
    public class ParmConfiguracion
    {
        public string company_name { get; set; } = "";
        public string? tax_id { get; set; }
        public string? address { get; set; }
        public string? phone { get; set; }
        public string? email { get; set; }
        public string? currency { get; set; }
        public decimal? tax_rate { get; set; }
        public int? quote_validity_days { get; set; }
        public string? default_terms { get; set; }
        // Marca para los documentos impresos
        public string? logo { get; set; }                  // data URL (PNG/JPEG)
        public string? slogan { get; set; }                // una línea por viñeta
        public string? mobile { get; set; }
        public string? city { get; set; }
    }

    public class ParmServicio
    {
        public Guid? id { get; set; }
        public string code { get; set; } = "";
        public string name { get; set; } = "";
        public int position { get; set; }
        public bool active { get; set; } = true;
    }

    // Plantilla de etapas de producción
    public class ParmEtapa
    {
        public Guid? id { get; set; }
        public string name { get; set; } = "";
        public int position { get; set; }
        public bool is_quality_check { get; set; }
        public bool active { get; set; } = true;
    }

    public class ParmSucursal
    {
        public Guid? id { get; set; }
        public string code { get; set; } = "";
        public string name { get; set; } = "";
        public string? address { get; set; }
        public string? phone { get; set; }
        public bool active { get; set; } = true;
    }
}
