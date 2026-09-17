# Module 01 architecture decision: stack choice

Status: Accepted
Date: 2026-09-17

Decision:
- Use .NET 8 with ASP.NET Core Web API for the backend.
- Use PostgreSQL as the primary relational database.
- Use Entity Framework Core with the Npgsql provider.
- Use a desktop-first local web application served from the local server over the LAN.
- Add a Windows installer for server and workstation deployment.
- Use a background worker/service for scheduled jobs and monitoring.

Rationale:
- Strong support for Windows-heavy local business environments.
- Simple path to LAN deployment and local printer/reporting workflows.
- Good PostgreSQL support.
- Maintainable architecture for future modules.
- Avoids SaaS-only dependencies while preserving a professional enterprise stack.

Consequences:
- The codebase will use a layered architecture with API, application, infrastructure, and domain projects.
- The initial project skeleton will be implemented before later module workflows.
- Installer and deployment decisions are held in the Module 01 implementation track.
