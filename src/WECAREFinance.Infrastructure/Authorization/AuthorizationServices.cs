using System.Security.Claims;
using WECAREFinance.Application.Authorization;

namespace WECAREFinance.Infrastructure.Authorization;

public sealed class CurrentUser : ICurrentUser
{
    private readonly ClaimsPrincipal _principal;

    public CurrentUser(ClaimsPrincipal principal)
    {
        _principal = principal;
    }

    public Guid? UserId
    {
        get
        {
            var value = _principal.FindFirstValue(ClaimTypes.NameIdentifier);
            return Guid.TryParse(value, out var id) ? id : null;
        }
    }

    public string? UserName => _principal.Identity?.Name;
    public bool IsAuthenticated => _principal.Identity?.IsAuthenticated == true;
    public bool IsPowerAdmin => _principal.IsInRole("PowerAdmin");

    public bool HasPermission(string permission) =>
        IsPowerAdmin || _principal.HasClaim("permission", permission);
}

public sealed class PermissionChecker : IPermissionChecker
{
    private readonly ICurrentUser _currentUser;

    public PermissionChecker(ICurrentUser currentUser)
    {
        _currentUser = currentUser;
    }

    public void Demand(string permission)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(permission);

        if (!_currentUser.IsAuthenticated || !_currentUser.HasPermission(permission))
        {
            throw new UnauthorizedAccessException($"Permission required: {permission}");
        }
    }
}
