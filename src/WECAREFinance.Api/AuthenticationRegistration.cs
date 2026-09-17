using Microsoft.AspNetCore.Identity;
using WECAREFinance.Application.Authentication;
using WECAREFinance.Domain.Entities;
using WECAREFinance.Infrastructure.Authentication;

namespace WECAREFinance.Api;

public static class AuthenticationRegistration
{
    public static IServiceCollection AddFoundationAuthentication(this IServiceCollection services)
    {
        services.AddScoped<IPasswordHasher<FinanceUser>, PasswordHasher<FinanceUser>>();
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        return services;
    }
}
