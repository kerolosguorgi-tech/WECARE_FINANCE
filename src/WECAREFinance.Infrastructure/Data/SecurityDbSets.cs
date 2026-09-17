using Microsoft.EntityFrameworkCore;
using WECAREFinance.Domain.Entities;

namespace WECAREFinance.Infrastructure;

public partial class AppDbContext
{
    public DbSet<FinanceUser> Users => Set<FinanceUser>();
    public DbSet<FinanceRole> Roles => Set<FinanceRole>();
    public DbSet<PermissionDefinition> Permissions => Set<PermissionDefinition>();
    public DbSet<UserRole> UserRoles => Set<UserRole>();
    public DbSet<RolePermission> RolePermissions => Set<RolePermission>();
    public DbSet<UserPermissionOverride> UserPermissionOverrides => Set<UserPermissionOverride>();
    public DbSet<UserSession> UserSessions => Set<UserSession>();
}
