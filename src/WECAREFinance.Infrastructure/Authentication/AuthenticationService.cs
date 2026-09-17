using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WECAREFinance.Application.Authentication;
using WECAREFinance.Domain.Entities;

namespace WECAREFinance.Infrastructure.Authentication;

public sealed class AuthenticationService : IAuthenticationService
{
    private const int MaxFailedAttempts = 5;
    private readonly AppDbContext _db;
    private readonly IPasswordHasher<FinanceUser> _passwordHasher;

    public AuthenticationService(AppDbContext db, IPasswordHasher<FinanceUser> passwordHasher)
    {
        _db = db;
        _passwordHasher = passwordHasher;
    }

    public async Task<LoginResult> LoginAsync(LoginRequest request, string? ipAddress, CancellationToken cancellationToken = default)
    {
        var user = await _db.Users.SingleOrDefaultAsync(x => x.UserName == request.UserName, cancellationToken);
        if (user is null || user.Status != UserAccountStatus.Active)
            return new(false, null, "Invalid credentials or inactive account.");

        var verification = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);
        if (verification == PasswordVerificationResult.Failed)
        {
            user.FailedLoginCount++;
            if (user.FailedLoginCount >= MaxFailedAttempts)
            {
                user.Status = UserAccountStatus.Locked;
                user.LockedAt = DateTimeOffset.UtcNow;
            }
            await _db.SaveChangesAsync(cancellationToken);
            return new(false, null, "Invalid credentials.");
        }

        user.FailedLoginCount = 0;
        user.LastLoginAt = DateTimeOffset.UtcNow;
        var rawToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(48));
        _db.UserSessions.Add(new UserSession
        {
            UserId = user.Id,
            TokenHash = Hash(rawToken),
            ExpiresAt = DateTimeOffset.UtcNow.AddHours(8),
            IpAddress = ipAddress
        });
        await _db.SaveChangesAsync(cancellationToken);
        return new(true, rawToken, null);
    }

    public async Task<bool> RevokeAsync(string token, CancellationToken cancellationToken = default)
    {
        var hash = Hash(token);
        var session = await _db.UserSessions.SingleOrDefaultAsync(x => x.TokenHash == hash && x.RevokedAt == null, cancellationToken);
        if (session is null) return false;
        session.RevokedAt = DateTimeOffset.UtcNow;
        await _db.SaveChangesAsync(cancellationToken);
        return true;
    }

    private static string Hash(string value)
    {
        return Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(value)));
    }
}
