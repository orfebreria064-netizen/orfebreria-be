namespace AURUM.Modelos.Aplicacion
{
    public class RespuestaProceso
    {
        public Int16 ProcesoSwitch { get; set; }          // 0: OK  ; 1:Error de Validacion  ; -1:Error Interno App / BD
        public Int32 ErrorNumero { get; set; }            // Codigo de Error (50000-50004 = regla de negocio, ver script SQL)
        public Int32 ErrorSeveridad { get; set; }         // Severidad de Error de BD
        public Int32 ErrorEstado { get; set; }            // Estado de Error de BD
        public string? ErrorProcedimiento { get; set; }   // Nombre de Procedimiento de Error de BD
        public Int32 ErrorLinea { get; set; }             // Numero de Linea de Error de BD
        public string? Mensaje { get; set; }              // Mensaje de respuesta (Satisfactorio, Validacion o Error Interno)
        public string? ResultProceso { get; set; }        // Resultado del procesamiento: id del registro creado/actualizado
        public object? Data { get; set; }                 // Fila devuelta por el procedimiento (opcional)
    }
}
