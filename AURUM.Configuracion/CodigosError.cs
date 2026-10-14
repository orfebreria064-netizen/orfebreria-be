namespace AURUM.Configuracion
{
    // Códigos que lanzan los procedimientos api.usp_* con THROW (ver Database/orfebreria_mssql.sql)
    public static class CodigosError
    {
        public const int ReglaNegocio = 50000;    // 400: mensaje listo para mostrar
        public const int SesionNoValida = 50001;  // 401: usuario inactivo o empresa suspendida
        public const int SoloLectura = 50002;     // 403: plan vencido
        public const int SinPermisos = 50003;     // 403
        public const int NoEncontrado = 50004;    // 404

        public static bool EsDeNegocio(int numero) => numero >= ReglaNegocio && numero <= NoEncontrado;

        public static int EstadoHttp(int numero) => numero switch
        {
            SesionNoValida => 401,
            SoloLectura => 403,
            SinPermisos => 403,
            NoEncontrado => 404,
            _ => 400
        };
    }
}
