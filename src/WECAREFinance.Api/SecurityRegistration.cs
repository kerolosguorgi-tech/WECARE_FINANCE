using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using WECAREFinance.Application.Authorization;
using WECAREFinance.Infrastructure.Authorization;

namespace WECAREFinance.Api;

public static class SecurityRegistration
{
    public static IServiceCollection AddFoundationSecurity(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddScoped<ICurrentUser>(provider =>
        {
            var accessor = provider.GetRequiredService<IHttpContextAccessor>();
            return new CurrentUser(accessor.HttpContext?.User ?? new ClaimsPrincipal());
        });
        services.AddScoped<IPermissionChecker, PermissionChecker>();
        services.AddAuthorization();
        return services;
    }
}
