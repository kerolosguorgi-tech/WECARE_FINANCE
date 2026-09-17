namespace WECAREFinance.Application.Authentication;

public sealed record LoginRequest(string UserName, string Password);
public sealed record LoginResult(bool Succeeded, string? Token, string? Error);

public interface IAuthenticationService
{
    Task<LoginResult> LoginAsync(LoginRequest request, string? ipAddress, CancellationToken cancellationToken = default);
    Task<bool> RevokeAsync(string token, CancellationToken cancellationToken = default);
}
