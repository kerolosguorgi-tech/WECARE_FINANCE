using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using WECAREFinance.Domain.Entities;

namespace WECAREFinance.Infrastructure;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<AuditLog> AuditLogs => Set<AuditLog>();
    public DbSet<WorkstationSettings> WorkstationSettings => Set<WorkstationSettings>();
    public DbSet<SystemSetting> SystemSettings => Set<SystemSetting>();
    public DbSet<ReferenceSequence> ReferenceSequences => Set<ReferenceSequence>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<AuditLog>(entity =>
        {
            entity.HasIndex(x => x.CorrelationId);
            entity.HasIndex(x => x.EntityType);
            entity.HasIndex(x => x.UserId);
            entity.Property(x => x.Action).HasMaxLength(200);
            entity.Property(x => x.EntityType).HasMaxLength(200);
            entity.Property(x => x.UserName).HasMaxLength(200);
            entity.Property(x => x.IpAddress).HasMaxLength(50);
        });

        modelBuilder.Entity<WorkstationSettings>(entity =>
        {
            entity.HasIndex(x => x.WorkstationKey).IsUnique();
            entity.Property(x => x.WorkstationKey).HasMaxLength(200);
            entity.Property(x => x.DisplayName).HasMaxLength(200);
            entity.Property(x => x.DefaultPrinter).HasMaxLength(200);
            entity.Property(x => x.DefaultExportFolder).HasMaxLength(500);
        });

        modelBuilder.Entity<SystemSetting>(entity =>
        {
            entity.HasIndex(x => x.Key).IsUnique();
            entity.Property(x => x.Key).HasMaxLength(200);
            entity.Property(x => x.Value).HasMaxLength(2000);
            entity.Property(x => x.Description).HasMaxLength(500);
        });

        modelBuilder.Entity<ReferenceSequence>(entity =>
        {
            entity.HasIndex(x => new { x.Prefix, x.Year }).IsUnique();
            entity.Property(x => x.Prefix).HasMaxLength(50);
        });
    }
}
