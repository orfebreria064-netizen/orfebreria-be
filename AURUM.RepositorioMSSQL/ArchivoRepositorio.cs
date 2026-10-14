using AURUM.Modelos.Aplicacion;
using Dapper;

namespace AURUM.RepositorioMSSQL;

public class ArchivoRepositorio : RepositorioMSSQL
{
    public ArchivoRepositorio(string cadenaConexionBD) : base(cadenaConexionBD) { }

    public RespuestaProceso Agregar(Guid idUsuario, Guid idPedido, string categoria, string nombreArchivo, string ruta,
                                    string? tipoMime, long tamanio, string? descripcion)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@user_id", idUsuario);
        parametros.Add("@order_id", idPedido);
        parametros.Add("@category", categoria);
        parametros.Add("@file_name", nombreArchivo);
        parametros.Add("@storage_path", ruta);
        parametros.Add("@mime_type", tipoMime);
        parametros.Add("@size_bytes", tamanio);
        parametros.Add("@description", descripcion);
        return base.Procesar("api.usp_attachment_add", parametros);
    }

    public RespuestaProceso Eliminar(Guid idUsuario, Guid idArchivo)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@user_id", idUsuario);
        parametros.Add("@id", idArchivo);
        return base.Procesar("api.usp_attachment_delete", parametros);
    }

    // Devuelve el archivo si el usuario puede verlo; null si no
    public Object? VerificarAcceso(Guid idUsuario, string ruta)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@user_id", idUsuario);
        parametros.Add("@storage_path", ruta);
        return base.TraerUno("api.usp_attachment_check_access", parametros);
    }
}
