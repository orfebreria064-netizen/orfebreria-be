using Microsoft.AspNetCore.Identity;

namespace AURUM.Aplicacion.Seguridad
{
    // Hash de contraseñas con PBKDF2 (PasswordHasher de ASP.NET Core Identity).
    // La base de datos nunca recibe la contraseña en claro.
    public static class ClaveAPL
    {
        static readonly PasswordHasher<object> hasher = new();
        static readonly object usuario = new();

        public const int LongitudMinima = 8;

        public static string Generar(string clave) => hasher.HashPassword(usuario, clave);

        public static bool Verificar(string hash, string clave) =>
            hasher.VerifyHashedPassword(usuario, hash, clave) != PasswordVerificationResult.Failed;

        public static string? Validar(string? clave) =>
            string.IsNullOrWhiteSpace(clave) || clave.Length < LongitudMinima
                ? $"La contraseña debe tener al menos {LongitudMinima} caracteres."
                : null;
    }
}
