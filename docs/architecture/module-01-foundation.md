# Module 01 — Foundation & Core Architecture

## Purpose

Establish the technical foundation for WECARE Finance before implementing business workflows.

## Required capabilities

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

This module must not invent or redefine the frozen business rules in the continuity roadmap. It must provide extension points for later modules without prematurely implementing their workflows.

## Architecture decisions still to freeze before coding

1. Backend language/framework.
2. UI approach: browser-based desktop-first client or packaged desktop shell.
3. Authentication/session strategy.
4. Migration tooling.
5. Installer technology and Windows service model.
6. Document storage layout and encryption-at-rest policy.
7. Supported Windows and PostgreSQL versions.

Technical defaults may be selected by the architect where they do not change business behavior, and must be recorded as ADRs.
