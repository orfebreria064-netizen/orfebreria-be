namespace AURUM.WebAPI.Models
{
    public class AppSeguridad
    {
        public string Key { get; set; } = "";        // Clave de firma JWT (mínimo 32 caracteres)
        public int HorasToken { get; set; } = 10;
    }
}
