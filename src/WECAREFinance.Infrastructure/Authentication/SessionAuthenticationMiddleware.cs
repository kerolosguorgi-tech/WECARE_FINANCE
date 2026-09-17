using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using WECAREFinance.Domain.Entities;

namespace WECAREFinance.Infrastructure.Authentication;

public sealed class SessionAuthenticationMiddleware
{
    private readonly RequestDelegate _next;

    public SessionAuthenticationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, AppDbContext db)
    {
        var rawToken = ReadBearerToken(context.Request);
        if (!string.IsNullOrWhiteSpace(rawToken))
        {
            var tokenHash = Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(rawToken)));
            var session = await db.UserSessions
                .Include(x => x.User)
                .ThenInclude(x => x.UserRoles)
                .ThenInclude(x => x.Role)
                .ThenInclude(x => x.RolePermissions)
                .ThenInclude(x => x.Permission)
                .Include(x => x.User)
                .ThenInclude(x => x.PermissionOverrides)
                .ThenInclude(x => x.Permission)
                .SingleOrDefaultAsync(x => x.TokenHash == tokenHash && x.RevokedAt == null && x.ExpiresAt > DateTimeOffset.UtcNow);

            if (session is not null && session.User.Status == UserAccountStatus.Active)
            {
                var claims = new List<Claim>
                {
                    new(ClaimTypes.NameIdentifier, session.User.Id.ToString()),
                    new(ClaimTypes.Name, session.User.UserName)
                };

                foreach (var role in session.User.UserRoles.Select(x => x.Role))
                {
                    claims.Add(new Claim(ClaimTypes.Role, role.Code));
                    foreach (var permission in role.RolePermissions.Select(x => x.Permission.Code))
                        claims.Add(new Claim("permission", permission));
                }

                foreach (var permissionOverride in session.User.PermissionOverrides)
                {
                    claims.Add(new Claim(
                        permissionOverride.IsGranted ? "permission" : "permission-denied",
                        permissionOverride.Permission.Code));
                }

                context.User = new ClaimsPrincipal(new ClaimsIdentity(claims, "WECARE.Session"));
            }
        }

        await _next(context);
    }

    private static string? ReadBearerToken(HttpRequest request)
    {
        var header = request.Headers.Authorization.ToString();
        if (header.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            return header[7..].Trim();
        return null;
    }
}
