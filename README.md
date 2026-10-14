# aurum-be

API REST (WebAPI .NET 8) de **Orfebrería Aurum**, plataforma SaaS multiempresa con sucursales.
Sigue la misma arquitectura que `argo-be`.

## Arquitectura

| Carpeta de solución | Proyecto | Responsabilidad |
|---|---|---|
| 01-WebAPI | `AURUM.WebAPI` | Controladores, JWT, Swagger, Serilog, rate limit, job de recordatorios |
| 02-Dominio | `AURUM.Modelos` | Parámetros (`Parm*`), `RespuestaProceso`, configuración |
| 03-Aplicacion | `AURUM.Aplicacion` | Clases `*APL`: contraseñas, correos, archivos y llamadas a repositorios |
| 04-Repositorio | `AURUM.RepositorioMSSQL` | Dapper sobre los procedimientos `api.usp_*` |
| 05-Util | `AURUM.Configuracion`, `AURUM.Notifica` | Códigos de error, envío de correos SMTP y plantillas |

Flujo: `Controller → *APL → *Repositorio → api.usp_*` (SQL Server).

Toda la lógica de negocio y la seguridad por **empresa, rol y sucursal** viven en los procedimientos
almacenados (`Database/orfebreria_mssql.sql`). La API no consulta tablas directamente.

## Seguridad

- Todos los endpoints exigen JWT, salvo los marcados `[AllowAnonymous]`: login, primer usuario, invitación,
  recuperación de contraseña, planes y solicitud de acceso.
- El usuario se toma **siempre del token** (`ClaimTypes.NameIdentifier`) y se envía como `@user_id` a cada
  procedimiento. Nunca se recibe en el body.
- Contraseñas con PBKDF2 (`PasswordHasher` de ASP.NET Core Identity). La base guarda solo el hash.
- La cuenta se bloquea 15 minutos después de 5 intentos fallidos (lo controla la base). Además, los endpoints
  públicos admiten 20 solicitudes por minuto por IP y endpoint.
- Los tokens de invitación y de recuperación los genera la base. Solo viajan por correo y nunca aparecen en
  las respuestas de la API.
- Los adjuntos se guardan en `RutaArchivos` y solo se sirven a quien puede ver el pedido. Se valida el tipo y
  el tamaño del archivo, y no se aceptan rutas fuera de esa carpeta (`../`).

## Respuestas

**Lecturas** (`listar*`, `traeruno`, reportes, dashboard): `200` con los datos. Los errores de la base
llegan como:

| Código SQL | HTTP | Significado |
|---|---|---|
| 50001 | 401 | Sesión no válida (usuario inactivo o empresa suspendida) |
| 50002 | 403 | Plan vencido: modo solo lectura |
| 50003 | 403 | Sin permisos |
| 50004 | 404 | No encontrado o sin acceso |
| 50000 | 400 | Regla de negocio |

**Escrituras** (`*prc`, `registrar`, `cambiarestado`…): `200` con `RespuestaProceso`, igual que en ARGO.

```json
{ "procesoSwitch": 0, "mensaje": "Proceso realizado correctamente.", "resultProceso": "<id>", "data": { ... } }
```

`procesoSwitch`: `0` OK · `1` validación (mostrar `mensaje`) · `-1` error interno. Si la sesión no es
válida (50001), la respuesta es `401`.

Las columnas se devuelven en `snake_case` (`customer_name`, `branch_id`…), igual que en Supabase.

## Puesta en marcha

1. Ejecutar `Database/orfebreria_mssql.sql` en `db_acfa96_orfebreria` (SQL Server 2017+, compatibilidad ≥ 130).
2. Copiar `AURUM.WebAPI/appsettings.Example.json` a `appsettings.Development.json` (no se versiona) y completar
   la conexión, la clave JWT (mínimo 32 caracteres), SMTP, `UrlFrontend` y `RutaArchivos`.
3. `dotnet run --project AURUM.WebAPI` y abrir `/swagger`.
4. Primer acceso: `POST api/Login/primerusuario` crea la empresa y su administrador, que además es el
   superadministrador de la plataforma. Solo funciona con la base vacía.

En producción, configurar los secretos con variables de entorno (p. ej. `SeguridadAPP__Key`,
`ConfiguracionApp__BD`) y definir `ConfiguracionApp:OrigenesPermitidos` con el dominio del frontend.

## Endpoints

| Controlador | Acciones |
|---|---|
| `Login` | `validausuario`, `primerusuario`, `invitacion/{token}`, `aceptarinvitacion`, `recuperarclave`, `restablecerclave`, `cambiarclave`, `sesion`, `actualizarperfil` |
| `Seguridad` | `listarusuarios`, `actualizarusuario`, `asignarsucursales`, `listarinvitaciones`, `invitar`, `reenviarinvitacion/{id}`, `revocarinvitacion/{id}` |
| `Configuracion` | `traerconfiguracion`, `actualizarconfiguracion`, `listarservicios`, `servicioprc`, `listaretapas`, `etapaprc`, `listarsucursales`, `sucursalprc`, `sucursalprincipal/{id}` |
| `Cliente` | `listarclientes`, `traeruno/{id}`, `clienteprc`, `contactoprc`, `eliminarcontacto/{id}` |
| `Material` | `listarmateriales`, `stocksucursales/{id}`, `materialprc`, `registrarmovimiento`, `trasladar`, `stockminimo`, `kardex`, `materialcliente/{idpedido}` |
| `Cotizacion` | `listarcotizaciones`, `traeruno/{id}`, `cotizacionprc`, `cambiarestado`, `duplicar/{id}`, `vencer`, `convertirpedido` |
| `Pedido` | `listarpedidos`, `traeruno/{id}`, `pedidoprc`, `cambiarestado`, `asignar`, `cronograma`, `generaretapas/{id}`, `agregaretapa`, `actualizaretapa`, `eliminaretapa/{id}`, `produccion` |
| `Pago` | `registrar`, `anular`, `listarpagos`, `porcobrar` |
| `Entrega` | `listarentregas`, `entregaprc`, `firma/{id}` (multipart) |
| `Archivo` | `subir/{idpedido}` (multipart), `descargar?ruta=`, `eliminar/{id}` |
| `Reporte` | `dashboard`, `ventas`, `cuentasporcobrar`, `cobranza`, `plazos`, `stock`, `custodia` |
| `Plataforma` | `planes`, `solicitaracceso`, `todosplanes`, `actualizarplan`, `solicitudes`, `aprobarsolicitud`, `rechazarsolicitud`, `invitaradmin`, `registrarpago`, `actualizarsuscripcion`, `empresas`, `resumen`, `pagossuscripcion` |

Cotizaciones y pedidos reciben `{ "id", "header": {...}, "items": [...] }`, con la misma forma que las
RPC `save_quotation` / `save_order` del frontend actual.

Los enlaces de los correos apuntan a `UrlFrontend/activar-cuenta?tipo=invitacion|recuperacion&token=...`.
