# Module 01 security validation

After applying the authentication migration, start the API and validate:

1. `POST /api/auth/login` with a valid user returns a token.
2. `GET /api/security/me` without `Authorization` returns `401`.
3. `GET /api/security/me` with `Authorization: Bearer <token>` returns the authenticated user.
4. `POST /api/auth/logout` with the bearer token revokes the session.
5. Calling `/api/security/me` with the revoked token returns `401`.
6. An expired, locked, or inactive user session is not accepted.

The current implementation loads roles, role permissions, and user permission overrides from PostgreSQL for each authenticated request. This is deliberately simple for the foundation; caching and session revocation optimization can be added after the security behavior is accepted.
