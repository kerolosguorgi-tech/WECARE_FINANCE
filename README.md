# WECARE Finance

Local/on-premise financial management platform for WECARE Medical LLC.

## Current checkpoint

- Phase: Architecture
- Module: 01 — Foundation & Core Architecture
- Status: In progress
- Coding: Not started
- Database schema: Not designed
- Deployment target: Windows-heavy local/LAN installation with PostgreSQL

The authoritative product requirements are maintained in the project continuity roadmap. The first implementation increment is the Module 01 architecture and foundation scaffold; business workflows are intentionally not implemented yet.

## Principles

- PostgreSQL is the primary relational database.
- Core financial operations work without permanent internet access.
- Permissions are enforced server-side.
- Posted financial records are immutable and corrected through controlled workflows.
- Auditability, traceability, reporting, and backup/recovery are platform capabilities.
- External integrations and AI are optional and isolated.
