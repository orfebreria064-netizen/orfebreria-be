using AURUM.Aplicacion.Base;
using AURUM.Aplicacion.Seguridad;
using AURUM.Modelos.Aplicacion;
using AURUM.Modelos.Plataforma;
using AURUM.Notifica;
using AURUM.RepositorioMSSQL;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace AURUM.Aplicacion.Plataforma;

// Plataforma SaaS: planes, solicitudes de acceso, suscripciones (superadministrador)
public class PlataformaAPL : AplicacionBase
{
    readonly IOptions<ConfiguracionAPP> configuracionAPP;

    public PlataformaAPL(IConfiguration iConfiguration, IOptions<ConfiguracionAPP> iConfiguracionAPP) : base(iConfiguration, iConfiguracionAPP)
    {
        configuracionAPP = iConfiguracionAPP;
    }

    public Object ListarPlanes(bool incluirInactivos)
    {
        PlataformaRepositorio _repositorio = new PlataformaRepositorio(parametrosApp.BD);
        return _repositorio.ListarPlanes(incluirInactivos);
    }

    public RespuestaProceso SolicitarAcceso(ParmSolicitudAcceso entidad)
    {
        PlataformaRepositorio _repositorio = new PlataformaRepositorio(parametrosApp.BD);
        return _repositorio.SolicitarAcceso(entidad);
    }

    public RespuestaProceso ActualizarPlan(Guid idUsuario, ParmPlan entidad)
    {
        PlataformaRepositorio _repositorio = new PlataformaRepositorio(parametrosApp.BD);
        return _repositorio.ActualizarPlan(idUsuario, entidad);
    }

    public Object ListarSolicitudes(Guid idUsuario, string? estado)
    {
        PlataformaRepositorio _repositorio = new PlataformaRepositorio(parametrosApp.BD);
        return _repositorio.ListarSolicitudes(idUsuario, estado);
    }

    // Crea la empresa e invita a su dueño como ADMIN (el correo sale con el enlace de activación)
    public RespuestaProceso AprobarSolicitud(Guid idUsuario, ParmAprobarSolicitud entidad)
    {
        PlataformaRepositorio _repositorio = new PlataformaRepositorio(parametrosApp.BD);
        var respuesta = _repositorio.AprobarSolicitud(idUsuario, entidad);
        return new UsuarioAPL(configuracion, configuracionAPP).EnviarInvitacion(respuesta);
    }

    public RespuestaProceso RechazarSolicitud(Guid idUsuario, ParmRechazarSolicitud entidad)
    {
        PlataformaRepositorio _repositorio = new PlataformaRepositorio(parametrosApp.BD);
        return _repositorio.RechazarSolicitud(idUsuario, entidad);
    }

    public RespuestaProceso InvitarAdmin(Guid idUsuario, ParmInvitarAdmin entidad)
    {
        PlataformaRepositorio _repositorio = new PlataformaRepositorio(parametrosApp.BD);
        var respuesta = _repositorio.InvitarAdmin(idUsuario, entidad);
        return new UsuarioAPL(configuracion, configuracionAPP).EnviarInvitacion(respuesta);
    }

    public RespuestaProceso RegistrarPago(Guid idUsuario, ParmPagoSuscripcion entidad)
    {
        PlataformaRepositorio _repositorio = new PlataformaRepositorio(parametrosApp.BD);
        return _repositorio.RegistrarPago(idUsuario, entidad);
    }

    public RespuestaProceso ActualizarSuscripcion(Guid idUsuario, ParmSuscripcion entidad)
    {
        PlataformaRepositorio _repositorio = new PlataformaRepositorio(parametrosApp.BD);
        return _repositorio.ActualizarSuscripcion(idUsuario, entidad);
    }

    public Object ListarEmpresas(Guid idUsuario)
    {
        PlataformaRepositorio _repositorio = new PlataformaRepositorio(parametrosApp.BD);
        return _repositorio.ListarEmpresas(idUsuario);
    }

    public Object? Resumen(Guid idUsuario)
    {
        PlataformaRepositorio _repositorio = new PlataformaRepositorio(parametrosApp.BD);
        return _repositorio.Resumen(idUsuario);
    }

    public Object PagosSuscripcion(Guid idUsuario, Guid? idEmpresa)
    {
        PlataformaRepositorio _repositorio = new PlataformaRepositorio(parametrosApp.BD);
        return _repositorio.PagosSuscripcion(idUsuario, idEmpresa);
    }

    // Avisos de vencimiento del día (lo llama el job RecordatorioJob). Devuelve los correos enviados.
    public int EnviarRecordatorios()
    {
        PlataformaRepositorio _repositorio = new PlataformaRepositorio(parametrosApp.BD);
        var correo = Correo();
        int enviados = 0;
        foreach (var r in _repositorio.RecordatoriosPendientes())
        {
            if (string.IsNullOrWhiteSpace(r.admin_emails)) continue;
            bool ok = correo.enviarCorreo(new MensajeNotificacion
            {
                emailDestino = r.admin_emails,
                asunto = $"Su plan {r.plan_name} - {r.company_name}",
                html_email_body = PlantillaCorreo.RecordatorioPlan(r.company_name, r.plan_name, r.state, r.days_left,
                                                                   r.current_period_end, r.grace_end)
            });
            if (ok) enviados++;
        }
        return enviados;
    }
}
