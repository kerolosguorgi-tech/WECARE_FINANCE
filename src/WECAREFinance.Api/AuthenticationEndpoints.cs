using WECAREFinance.Application.Authentication;

namespace WECAREFinance.Api;

public static class AuthenticationEndpoints
{
    public static IEndpointRouteBuilder MapFoundationAuthentication(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/api/auth/login", async (LoginRequest request, IAuthenticationService service, HttpContext context, CancellationToken cancellationToken) =>
        {
            var result = await service.LoginAsync(request, context.Connection.RemoteIpAddress?.ToString(), cancellationToken);
            return result.Succeeded ? Results.Ok(result) : Results.Unauthorized();
        });

        endpoints.MapPost("/api/auth/logout", async (HttpContext context, IAuthenticationService service, CancellationToken cancellationToken) =>
        {
            var header = context.Request.Headers.Authorization.ToString();
            var token = header.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase) ? header[7..] : header;
            return await service.RevokeAsync(token, cancellationToken) ? Results.NoContent() : Results.NotFound();
        });

        return endpoints;
    }
}
