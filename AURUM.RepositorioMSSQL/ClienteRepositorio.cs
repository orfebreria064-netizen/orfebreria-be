using AURUM.Modelos.Aplicacion;
using AURUM.Modelos.Clientes;
using Dapper;

namespace AURUM.RepositorioMSSQL;

public class ClienteRepositorio : RepositorioMSSQL
{
    public ClienteRepositorio(string cadenaConexionBD) : base(cadenaConexionBD) { }

    public Object Listar(Guid idUsuario, ParmClienteFiltro filtro)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@user_id", idUsuario);
        parametros.Add("@search", filtro.search);
        parametros.Add("@include_inactive", filtro.include_inactive);
        return base.Listar("api.usp_customers_list", parametros);
    }

    // Ficha 360°: cliente, resumen, contactos y pedidos
    public Object TraerUno(Guid idUsuario, Guid idCliente)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@user_id", idUsuario);
        parametros.Add("@id", idCliente);
        return base.ListarMultiple("api.usp_customer_get", parametros, new[] { "cliente", "resumen", "contactos", "pedidos" }, 2);
    }

    public RespuestaProceso ClientePrc(Guid idUsuario, ParmCliente entidad)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@user_id", idUsuario);
        parametros.Add("@id", entidad.id);
        parametros.Add("@person_type", entidad.person_type);
        parametros.Add("@first_name", entidad.first_name);
        parametros.Add("@last_name", entidad.last_name);
        parametros.Add("@business_name", entidad.business_name);
        parametros.Add("@document_type", entidad.document_type);
        parametros.Add("@document_number", entidad.document_number);
        parametros.Add("@phone", entidad.phone);
        parametros.Add("@whatsapp", entidad.whatsapp);
        parametros.Add("@email", entidad.email);
        parametros.Add("@address", entidad.address);
        parametros.Add("@city", entidad.city);
        parametros.Add("@notes", entidad.notes);
        parametros.Add("@active", entidad.active);
        return base.Procesar("api.usp_customer_save", parametros);
    }

    public RespuestaProceso ContactoPrc(Guid idUsuario, ParmContacto entidad)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@user_id", idUsuario);
        parametros.Add("@id", entidad.id);
        parametros.Add("@customer_id", entidad.customer_id);
        parametros.Add("@name", entidad.name);
        parametros.Add("@position", entidad.position);
        parametros.Add("@phone", entidad.phone);
        parametros.Add("@email", entidad.email);
        parametros.Add("@notes", entidad.notes);
        return base.Procesar("api.usp_customer_contact_save", parametros);
    }

    public RespuestaProceso EliminarContacto(Guid idUsuario, Guid idContacto)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@user_id", idUsuario);
        parametros.Add("@id", idContacto);
        return base.Procesar("api.usp_customer_contact_delete", parametros);
    }
}
