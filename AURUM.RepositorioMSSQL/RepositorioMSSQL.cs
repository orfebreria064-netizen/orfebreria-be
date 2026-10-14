using AURUM.Configuracion;
using AURUM.Modelos.Aplicacion;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Text.Json;

namespace AURUM.RepositorioMSSQL
{
    // Base de los repositorios: todo el acceso a datos es por procedimientos almacenados (api.usp_*).
    // Los procedimientos validan empresa, rol y sucursal del @user_id y lanzan THROW 50000-50004
    // con un mensaje listo para mostrar.
    //   - Consultas (Listar, TraerUno, ListarMultiple, TraerJson): el error de negocio sube como SqlException
    //     y el controlador lo convierte en el estado HTTP que corresponde.
    //   - Escrituras (Procesar): el error se devuelve en RespuestaProceso, igual que en ARGO.
    public class RepositorioMSSQL
    {
        readonly string _cadenaConexion;

        public RepositorioMSSQL(string cadenaConexionBD)
        {
            _cadenaConexion = cadenaConexionBD;
        }

        SqlConnection Conexion() => new SqlConnection(_cadenaConexion);

        public Object Listar(string uspBD, DynamicParameters parameters)
        {
            using var con = Conexion();
            return con.Query(uspBD, param: parameters, commandType: CommandType.StoredProcedure).AsList();
        }

        public List<T> Listar<T>(string uspBD, DynamicParameters parameters)
        {
            using var con = Conexion();
            return con.Query<T>(uspBD, param: parameters, commandType: CommandType.StoredProcedure).AsList();
        }

        public Object? TraerUno(string uspBD, DynamicParameters parameters)
        {
            using var con = Conexion();
            return con.QueryFirstOrDefault(uspBD, param: parameters, commandType: CommandType.StoredProcedure);
        }

        public T? TraerUno<T>(string uspBD, DynamicParameters parameters)
        {
            using var con = Conexion();
            return con.QueryFirstOrDefault<T>(uspBD, param: parameters, commandType: CommandType.StoredProcedure);
        }

        // Procedimientos con varios resultados (p. ej. pedido + ítems + etapas...).
        // Los primeros "registrosUnicos" resultados se devuelven como un objeto; el resto como listas.
        public Dictionary<string, Object?> ListarMultiple(string uspBD, DynamicParameters parameters, string[] nombres, int registrosUnicos = 1)
        {
            var resultado = new Dictionary<string, Object?>();
            using var con = Conexion();
            using var lector = con.QueryMultiple(uspBD, param: parameters, commandType: CommandType.StoredProcedure);
            for (int i = 0; i < nombres.Length && !lector.IsConsumed; i++)
            {
                var filas = lector.Read().AsList();
                resultado[nombres[i]] = i < registrosUnicos ? filas.FirstOrDefault() : filas;
            }
            return resultado;
        }

        // Procedimientos que devuelven un JSON armado en la base (dashboard, resumen de plataforma)
        public Object? TraerJson(string uspBD, DynamicParameters parameters)
        {
            using var con = Conexion();
            var json = con.QueryFirstOrDefault<string>(uspBD, param: parameters, commandType: CommandType.StoredProcedure);
            return string.IsNullOrEmpty(json) ? null : JsonSerializer.Deserialize<JsonElement>(json);
        }

        public RespuestaProceso Procesar(string uspBD, DynamicParameters parameters)
        {
            RespuestaProceso respuestaProceso = new RespuestaProceso();
            try
            {
                using var con = Conexion();
                var fila = con.QueryFirstOrDefault(uspBD, param: parameters, commandType: CommandType.StoredProcedure)
                           as IDictionary<string, object?>;

                respuestaProceso.ProcesoSwitch = 0;
                respuestaProceso.Mensaje = "Proceso realizado correctamente.";
                if (fila != null)
                {
                    respuestaProceso.Data = fila;
                    respuestaProceso.ResultProceso = fila.Values.FirstOrDefault()?.ToString();
                }
            }
            catch (SqlException ex)
            {
                respuestaProceso.ProcesoSwitch = (Int16)(CodigosError.EsDeNegocio(ex.Number) ? 1 : -1);
                respuestaProceso.ErrorNumero = ex.Number;
                respuestaProceso.ErrorSeveridad = ex.Class;
                respuestaProceso.ErrorEstado = ex.State;
                respuestaProceso.ErrorProcedimiento = ex.Procedure;
                respuestaProceso.ErrorLinea = ex.LineNumber;
                respuestaProceso.Mensaje = ex.Message;
            }
            return respuestaProceso;
        }
    }
}
