# TecnoGas Hogar — Portal de Solicitudes de Servicio Técnico

Prototipo web (ASP.NET Core 10 MVC + EF Core + SQLite) para registrar y consultar
solicitudes de servicio técnico de "TecnoGas Hogar".

Evaluación Continua 1 — Ciclo 2026-2.

## Funcionalidades

- Registro de una nueva solicitud de servicio (Insert).
- Listado de las solicitudes registradas (Select).

## Tecnologías

- ASP.NET Core 10 MVC (C#)
- Entity Framework Core 10 + SQLite
- Docker (despliegue en Render)

## Cómo ejecutar en local

```bash
dotnet restore
dotnet tool restore
dotnet ef database update
dotnet run
```

La aplicación quedará disponible en la URL que indique la consola (por ejemplo
`http://localhost:5000`).

## Estructura de ramas del repositorio

- `main` — versión estable.
- `develop` — integración de funcionalidades.
- `feature/modelo-sqlite` — entidad `SolicitudServicio`, `DbContext` y migración inicial.
- `feature/registro-solicitud` — formulario e inserción de solicitudes.
- `feature/listado-solicitudes` — listado de solicitudes registradas.

## Despliegue en Render

La aplicación se despliega en Render como **Web Service** usando el `Dockerfile` incluido
en la raíz del repositorio.

### Pasos para desplegar

1. Entrar a [render.com](https://render.com) e iniciar sesión (se puede usar la cuenta de GitHub).
2. **New +** → **Web Service**.
3. Conectar el repositorio `evaluacion20262` de GitHub.
4. Configuración del servicio:
   - **Language / Environment**: `Docker` (Render detecta el `Dockerfile` automáticamente).
   - **Branch**: `main`.
   - **Instance Type**: `Free`.
5. No se requieren variables de entorno adicionales para que la app funcione: la cadena de
   conexión SQLite (`Data Source=tecnogas.db`) ya está definida en `appsettings.json`, y las
   migraciones de EF Core se aplican automáticamente al iniciar la aplicación
   (`db.Database.Migrate()` en `Program.cs`), así que la base de datos y la tabla se crean
   solas en el contenedor sin pasos manuales.
6. **Create Web Service**. Render construye la imagen a partir del `Dockerfile` y publica la
   app en una URL pública del tipo `https://evaluacion20262.onrender.com`.

### Configuración técnica del Dockerfile

- Build multi-stage: compila con `mcr.microsoft.com/dotnet/sdk:10.0` y ejecuta con la imagen
  liviana `mcr.microsoft.com/dotnet/aspnet:10.0`.
- El contenedor expone el puerto **8080** (`ASPNETCORE_HTTP_PORTS=8080`), que es el puerto
  que Render detecta automáticamente a través del `EXPOSE 8080` del Dockerfile.
- `DOTNET_EnableWriteXorExecute=0`: evita que el runtime de .NET falle con `SIGSEGV` (exit
  code 139) al iniciar dentro del sandbox de contenedores de Render.
- `DOTNET_hostBuilder__reloadConfigOnChange=false`: desactiva el `FileSystemWatcher` que
  ASP.NET Core usa para recargar `appsettings.json` en caliente. El sandbox de Render limita
  fuertemente los `inotify` del sistema, y esa vigilancia de archivos hacía crashear la app
  apenas arrancaba (`IOException: configured user limit on inotify instances`).

### Nota sobre SQLite en Render (plan Free)

El plan gratuito de Render usa disco efímero: si el servicio se reinicia o se hace un nuevo
deploy, el archivo `tecnogas.db` se recrea vacío (las migraciones se vuelven a aplicar solas
al iniciar, así que la app nunca se rompe, pero los datos previos no persisten entre
despliegues). Para este prototipo académico es aceptable; si se necesitara persistencia real,
Render ofrece **Persistent Disks** en sus planes pagos.

### URL pública

`https://<pendiente-de-completar-tras-el-deploy>.onrender.com`
