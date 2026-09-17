using Microsoft.EntityFrameworkCore;
using WECAREFinance.Domain.Entities;

namespace WECAREFinance.Infrastructure;

public partial class AppDbContext
{
    private void ConfigureSecurity(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<FinanceUser>(entity =>
        {
            entity.HasIndex(x => x.UserName).IsUnique();
            entity.Property(x => x.UserName).HasMaxLength(100).IsRequired();
            entity.Property(x => x.DisplayName).HasMaxLength(200).IsRequired();
            entity.Property(x => x.PasswordHash).HasMaxLength(500).IsRequired();
            entity.HasMany(x => x.UserRoles).WithOne(x => x.User).HasForeignKey(x => x.UserId);
            entity.HasMany(x => x.PermissionOverrides).WithOne(x => x.User).HasForeignKey(x => x.UserId);
        });

        modelBuilder.Entity<FinanceRole>(entity =>
        {
            entity.HasIndex(x => x.Code).IsUnique();
            entity.Property(x => x.Code).HasMaxLength(100).IsRequired();
            entity.Property(x => x.Name).HasMaxLength(200).IsRequired();
        });

        modelBuilder.Entity<PermissionDefinition>(entity =>
        {
            entity.HasIndex(x => x.Code).IsUnique();
            entity.Property(x => x.Code).HasMaxLength(150).IsRequired();
            entity.Property(x => x.Name).HasMaxLength(200).IsRequired();
            entity.Property(x => x.Description).HasMaxLength(500);
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(x => new { x.UserId, x.RoleId });
            entity.HasOne(x => x.User).WithMany(x => x.UserRoles).HasForeignKey(x => x.UserId);
            entity.HasOne(x => x.Role).WithMany(x => x.UserRoles).HasForeignKey(x => x.RoleId);
        });

        modelBuilder.Entity<RolePermission>(entity =>
        {
            entity.HasKey(x => new { x.RoleId, x.PermissionId });
            entity.HasOne(x => x.Role).WithMany(x => x.RolePermissions).HasForeignKey(x => x.RoleId);
            entity.HasOne(x => x.Permission).WithMany(x => x.RolePermissions).HasForeignKey(x => x.PermissionId);
        });

        modelBuilder.Entity<UserPermissionOverride>(entity =>
        {
            entity.HasKey(x => new { x.UserId, x.PermissionId });
            entity.HasOne(x => x.User).WithMany(x => x.PermissionOverrides).HasForeignKey(x => x.UserId);
            entity.HasOne(x => x.Permission).WithMany(x => x.RolePermissions).HasForeignKey(x => x.PermissionId).OnDelete(DeleteBehavior.Restrict);
            entity.Property(x => x.Reason).HasMaxLength(500);
        });

        modelBuilder.Entity<UserSession>(entity =>
        {
            entity.HasIndex(x => x.TokenHash).IsUnique();
            entity.HasIndex(x => new { x.UserId, x.RevokedAt, x.ExpiresAt });
            entity.Property(x => x.TokenHash).HasMaxLength(128).IsRequired();
            entity.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId);
        });
    }
}
