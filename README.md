# WECARE Finance

Local/on-premise financial management platform for WECARE Medical LLC.

## Current checkpoint

- Phase: Foundation implementation
- Module: 01 — Foundation & Core Architecture
- Status: Foundation scaffold implemented; verification pending in CI/local environment
- Backend: ASP.NET Core 8 minimal API
- Database: PostgreSQL via Npgsql and EF Core migrations
- Deployment target: Windows-heavy local/LAN installation with PostgreSQL

The authoritative product requirements are maintained in the project continuity roadmap. Business workflows remain intentionally unimplemented until their module checkpoints are reached.

## Principles

- PostgreSQL is the primary relational database.
- Core financial operations work without permanent internet access.
- Permissions are enforced server-side.
- Posted financial records are immutable and corrected through controlled workflows.
- Auditability, traceability, reporting, and backup/recovery are platform capabilities.
- External integrations and AI are optional and isolated.
