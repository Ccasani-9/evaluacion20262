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
