using Microsoft.EntityFrameworkCore;

namespace WECARE.Finance.Api.Persistence;

public sealed class FinanceDbContext(DbContextOptions<FinanceDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("finance");
        modelBuilder.Entity<FoundationMetadata>(entity =>
        {
            entity.ToTable("foundation_metadata");
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Id).ValueGeneratedNever();
            entity.Property(x => x.SchemaVersion).IsRequired();
            entity.Property(x => x.CreatedAtUtc).IsRequired();
        });
    }
}

public sealed class FoundationMetadata
{
    public int Id { get; set; }
    public string SchemaVersion { get; set; } = "1";
    public DateTime CreatedAtUtc { get; set; }
}
