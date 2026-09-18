# ADR-001: Foundation technology stack

- Status: Accepted for Module 01 foundation implementation
- Date: 2026-09-18

## Decision

Use a .NET 8 ASP.NET Core minimal API as the server-side application boundary, EF Core 8 with Npgsql for PostgreSQL access and versioned migrations, and xUnit for automated tests. The initial client boundary is HTTP/JSON so a browser-based desktop-first client or packaged Windows shell can be added without coupling business rules to presentation.

## Rationale

- .NET has a supported Windows deployment path and is suitable for a local/LAN service.
- ASP.NET Core provides health checks, structured logging, configuration providers, authentication/authorization middleware, and background-service extension points.
- PostgreSQL remains the authoritative relational store.
- EF Core migrations provide a reviewable, repeatable schema evolution mechanism.
- Keeping the initial API small avoids implementing later financial workflows prematurely.

## Initial operational defaults

- Configuration is environment-specific and supplied by `appsettings*.json` plus environment variables.
- `ConnectionStrings__Finance` supplies the PostgreSQL connection string.
- `/health/live` is process-only; `/health/ready` verifies PostgreSQL connectivity.
- The database schema is `finance`.
- No credentials, secrets, or production connection strings are committed.

## Explicitly deferred

Authentication providers, role/permission policies, document encryption policy, installer technology, Windows service registration, supported PostgreSQL minor version, and UI packaging require later approved decisions. This scaffold provides extension points without claiming those decisions are frozen.
