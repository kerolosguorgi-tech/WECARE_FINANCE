using System.Security.Claims;
using WECAREFinance.Application.Authorization;
using WECAREFinance.Infrastructure.Authorization;

namespace WECAREFinance.UnitTests;

public class AuthorizationTests
{
    [Fact]
    public void PowerAdmin_ShouldHaveEveryPermission()
    {
        var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Role, "PowerAdmin") }, "test");
        var user = new CurrentUser(new ClaimsPrincipal(identity));

        Assert.True(user.IsPowerAdmin);
        Assert.True(user.HasPermission(Permissions.Override));
    }

    [Fact]
    public void UnauthenticatedUser_ShouldNotHavePermission()
    {
        var user = new CurrentUser(new ClaimsPrincipal(new ClaimsIdentity()));

        Assert.False(user.IsAuthenticated);
        Assert.False(user.HasPermission(Permissions.SystemView));
    }
}
