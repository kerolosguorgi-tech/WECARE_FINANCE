# Module 01 — Foundation & Core Architecture

## Purpose

Establish the technical foundation for WECARE Finance before implementing business workflows.

## Implemented increment

- ASP.NET Core 8 API host
- PostgreSQL connectivity through EF Core/Npgsql
- Versioned EF Core migration for the foundation schema
- Liveness and readiness health checks
- JSON console logging and environment configuration
- Test project with host-independent foundation checks

## Required capabilities and extension points

- Local/on-premise deployment model
- PostgreSQL connectivity and versioned migrations
- Application/API/business-layer boundary
- Configuration and environment separation
- Logging, error handling, and health checks
- Authentication and permission infrastructure skeleton
- Audit infrastructure skeleton
- Transaction/reference-number service abstraction
- Document storage abstraction
- Reporting abstraction
- Background job abstraction
- Workstation identity/configuration concept
- Backup and recovery hooks

## Constraints

This module must not invent or redefine frozen business rules in the continuity roadmap. The migration only creates foundation metadata and does not create accounting or other business workflows.

## Verification commands

```powershell
dotnet restore
dotnet build
dotnet test
$env:ConnectionStrings__Finance = "Host=localhost;Port=5432;Database=wecare_finance;Username=...;Password=..."
dotnet run --project src/WECARE.Finance.Api
```

Readiness requires a reachable PostgreSQL instance and a database user permitted to access the `finance` schema.
