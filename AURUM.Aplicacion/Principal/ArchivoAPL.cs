using AURUM.Aplicacion.Base;
using AURUM.Modelos.Aplicacion;
using AURUM.RepositorioMSSQL;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace AURUM.Aplicacion.Principal;

// Adjuntos de pedidos y firmas de entrega. El archivo se guarda en RutaArchivos y la base
// solo registra la metadata; antes de servir un archivo se valida el acceso del usuario.
public class ArchivoAPL : AplicacionBase
{
    static readonly HashSet<string> TiposPermitidos = new(StringComparer.OrdinalIgnoreCase)
    {
        "image/jpeg", "image/png", "image/webp", "image/gif", "image/svg+xml", "application/pdf",
        "application/msword", "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
        "application/vnd.ms-excel", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
    };
    static readonly HashSet<string> TiposFirma = new(StringComparer.OrdinalIgnoreCase) { "image/png", "image/jpeg" };

    public ArchivoAPL(IConfiguration iConfiguration, IOptions<ConfiguracionAPP> iConfiguracionAPP) : base(iConfiguration, iConfiguracionAPP)
    { }

    public RespuestaProceso Subir(Guid idUsuario, Guid idPedido, string categoria, string nombreArchivo, string? tipoMime,
                                  long tamanio, Stream contenido, string? descripcion)
    {
        var error = ValidarArchivo(tipoMime, tamanio, TiposPermitidos);
        if (error != null) return new RespuestaProceso { ProcesoSwitch = 1, Mensaje = error };

        var ruta = $"{idPedido}/{Guid.NewGuid():N}{Path.GetExtension(nombreArchivo).ToLowerInvariant()}";
        Guardar(ruta, contenido);

        ArchivoRepositorio _repositorio = new ArchivoRepositorio(parametrosApp.BD);
        var respuesta = _repositorio.Agregar(idUsuario, idPedido, categoria, Path.GetFileName(nombreArchivo), ruta, tipoMime, tamanio, descripcion);
        if (respuesta.ProcesoSwitch != 0) File.Delete(RutaCompleta(ruta));
        return respuesta;
    }

    public RespuestaProceso SubirFirma(Guid idUsuario, Guid idEntrega, string? tipoMime, long tamanio, Stream contenido)
    {
        var error = ValidarArchivo(tipoMime, tamanio, TiposFirma);
        if (error != null) return new RespuestaProceso { ProcesoSwitch = 1, Mensaje = error };

        var ruta = $"firmas/{idEntrega}/{Guid.NewGuid():N}{(tipoMime == "image/png" ? ".png" : ".jpg")}";
        Guardar(ruta, contenido);

        EntregaRepositorio _repositorio = new EntregaRepositorio(parametrosApp.BD);
        var respuesta = _repositorio.Firma(idUsuario, idEntrega, ruta);
        if (respuesta.ProcesoSwitch != 0) File.Delete(RutaCompleta(ruta));
        else respuesta.ResultProceso = ruta;
        return respuesta;
    }

    // null si el usuario no puede ver el archivo o ya no existe
    public ArchivoDescarga? Descargar(Guid idUsuario, string ruta)
    {
        ArchivoRepositorio _repositorio = new ArchivoRepositorio(parametrosApp.BD);
        var archivo = _repositorio.VerificarAcceso(idUsuario, ruta);
        if (archivo == null) return null;

        var completa = RutaCompleta(ruta);
        if (!File.Exists(completa)) return null;
        return new ArchivoDescarga
        {
            ruta_completa = completa,
            tipo_mime = Valor(archivo, "mime_type") ?? "application/octet-stream",
            nombre = Valor(archivo, "file_name") ?? Path.GetFileName(completa)
        };
    }

    // Borrado lógico: el archivo físico se conserva para auditoría
    public RespuestaProceso Eliminar(Guid idUsuario, Guid idArchivo)
    {
        ArchivoRepositorio _repositorio = new ArchivoRepositorio(parametrosApp.BD);
        return _repositorio.Eliminar(idUsuario, idArchivo);
    }

    string? ValidarArchivo(string? tipoMime, long tamanio, HashSet<string> permitidos)
    {
        if (tamanio <= 0) return "El archivo está vacío.";
        if (tamanio > parametrosApp.ArchivoMaxMB * 1024L * 1024L) return $"El archivo supera el máximo de {parametrosApp.ArchivoMaxMB} MB.";
        if (string.IsNullOrEmpty(tipoMime) || !permitidos.Contains(tipoMime)) return "Tipo de archivo no permitido.";
        return null;
    }

    void Guardar(string ruta, Stream contenido)
    {
        var completa = RutaCompleta(ruta);
        Directory.CreateDirectory(Path.GetDirectoryName(completa)!);
        using var destino = File.Create(completa);
        contenido.CopyTo(destino);
    }

    // Impide rutas fuera de la carpeta de archivos ("../")
    string RutaCompleta(string ruta)
    {
        var raiz = Path.GetFullPath(parametrosApp.RutaArchivos);
        var completa = Path.GetFullPath(Path.Combine(raiz, ruta));
        if (!completa.StartsWith(raiz.TrimEnd(Path.DirectorySeparatorChar) + Path.DirectorySeparatorChar, StringComparison.OrdinalIgnoreCase))
            throw new UnauthorizedAccessException("Ruta de archivo no válida.");
        return completa;
    }
}
