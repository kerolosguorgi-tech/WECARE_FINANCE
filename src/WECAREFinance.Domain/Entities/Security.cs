using WECAREFinance.Domain.Common;

namespace WECAREFinance.Domain.Entities;

public enum UserAccountStatus
{
    Active = 1,
    Inactive = 2,
    Locked = 3
}

public class FinanceUser : EntityBase
{
    public string UserName { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public UserAccountStatus Status { get; set; } = UserAccountStatus.Active;
    public int FailedLoginCount { get; set; }
    public DateTimeOffset? LockedAt { get; set; }
    public DateTimeOffset? LastLoginAt { get; set; }
    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    public ICollection<UserPermissionOverride> PermissionOverrides { get; set; } = new List<UserPermissionOverride>();
}

public class FinanceRole : EntityBase
{
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public bool IsProtected { get; set; }
    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    public ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
}

public class PermissionDefinition : EntityBase
{
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
}

public class UserRole
{
    public Guid UserId { get; set; }
    public FinanceUser User { get; set; } = null!;
    public Guid RoleId { get; set; }
    public FinanceRole Role { get; set; } = null!;
}

public class RolePermission
{
    public Guid RoleId { get; set; }
    public FinanceRole Role { get; set; } = null!;
    public Guid PermissionId { get; set; }
    public PermissionDefinition Permission { get; set; } = null!;
}

public class UserPermissionOverride
{
    public Guid UserId { get; set; }
    public FinanceUser User { get; set; } = null!;
    public Guid PermissionId { get; set; }
    public PermissionDefinition Permission { get; set; } = null!;
    public bool IsGranted { get; set; }
    public string? Reason { get; set; }
}

public class UserSession : EntityBase
{
    public Guid UserId { get; set; }
    public FinanceUser User { get; set; } = null!;
    public string TokenHash { get; set; } = string.Empty;
    public DateTimeOffset ExpiresAt { get; set; }
    public DateTimeOffset? RevokedAt { get; set; }
    public string? IpAddress { get; set; }
}
