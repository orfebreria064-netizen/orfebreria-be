using AURUM.Modelos.Seguridad;

namespace AURUM.WebAPI.Services
{
    public interface IAutenticaService
    {
        string Autentica(RespuestaLogin usuario);
    }
}
