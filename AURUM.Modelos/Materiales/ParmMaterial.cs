namespace AURUM.Modelos.Materiales
{
    public class ParmMaterial
    {
        public Guid? id { get; set; }
        public string name { get; set; } = "";
        public string type { get; set; } = "";
        public string? purity { get; set; }
        public string unit { get; set; } = "g";
        public decimal min_stock { get; set; }
        public decimal reference_cost { get; set; }
        public bool active { get; set; } = true;
        public decimal initial_stock { get; set; }       // Solo en el alta: se registra como ENTRADA
        public Guid? branch_id { get; set; }              // Sucursal del stock inicial
    }

    public class ParmMaterialFiltro
    {
        public Guid? branch_id { get; set; }              // null = stock consolidado
        public bool include_inactive { get; set; }
        public string? type { get; set; }
    }

    public class ParmMovimiento
    {
        public Guid material_id { get; set; }
        public string movement_type { get; set; } = "";   // ENTRADA | SALIDA | AJUSTE | CONSUMO | DEVOLUCION
        public decimal quantity { get; set; }
        public string ownership { get; set; } = "EMPRESA"; // EMPRESA | CLIENTE
        public Guid? order_id { get; set; }
        public Guid? branch_id { get; set; }
        public decimal? unit_cost { get; set; }
        public DateTime? movement_date { get; set; }
        public string? notes { get; set; }
    }

    public class ParmTraslado
    {
        public Guid material_id { get; set; }
        public Guid from_branch_id { get; set; }
        public Guid to_branch_id { get; set; }
        public decimal quantity { get; set; }
        public string? notes { get; set; }
    }

    public class ParmStockMinimo
    {
        public Guid material_id { get; set; }
        public Guid branch_id { get; set; }
        public decimal min_stock { get; set; }
    }

    // Movimientos de material (kárdex de uno o todos los materiales)
    public class ParmKardex
    {
        public Guid? material_id { get; set; }
        public Guid? branch_id { get; set; }
        public Guid? order_id { get; set; }
        public string? movement_type { get; set; }
        public DateTime? from { get; set; }
        public DateTime? to { get; set; }
        public int? limit { get; set; }
    }

    // Precios del día (S/ por unidad del material)
    public class ParmPrecios
    {
        public DateTime? price_date { get; set; }          // null = hoy
        public List<ParmPrecioMaterial> prices { get; set; } = new();
    }

    public class ParmPrecioMaterial
    {
        public Guid material_id { get; set; }
        public decimal price { get; set; }
    }
}
