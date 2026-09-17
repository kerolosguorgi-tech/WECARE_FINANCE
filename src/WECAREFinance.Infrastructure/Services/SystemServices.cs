using Microsoft.EntityFrameworkCore;
using WECAREFinance.Application.Abstractions;
using WECAREFinance.Domain.Entities;

namespace WECAREFinance.Infrastructure.Services;

public class ReferenceNumberService : IReferenceNumberService
{
    public string Generate(string prefix, DateTimeOffset? timestamp = null)
    {
        var effectiveTimestamp = timestamp ?? DateTimeOffset.UtcNow;
        var year = effectiveTimestamp.ToLocalTime().Year;
        var sequence = Random.Shared.NextInt64(100000, 999999);
        return $"{prefix.ToUpperInvariant()}-{year}-{sequence}";
    }
}

public class AuditService : IAuditService
{
    private readonly AppDbContext _dbContext;

    public AuditService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task WriteAsync(AuditLog auditLog, CancellationToken cancellationToken = default)
    {
        _dbContext.AuditLogs.Add(auditLog);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}

public class WorkstationSettingsService : IWorkstationSettingsService
{
    private readonly AppDbContext _dbContext;

    public WorkstationSettingsService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<WorkstationSettings> GetAsync(string workstationKey, CancellationToken cancellationToken = default)
    {
        var settings = await _dbContext.WorkstationSettings
            .FirstOrDefaultAsync(x => x.WorkstationKey == workstationKey && x.IsActive, cancellationToken);

        return settings ?? new WorkstationSettings
        {
            WorkstationKey = workstationKey,
            DisplayName = workstationKey,
            DefaultPrinter = string.Empty,
            DefaultExportFolder = string.Empty,
            PaperSize = "A4",
            Orientation = "Portrait",
            IsActive = true
        };
    }

    public async Task SaveAsync(WorkstationSettings settings, CancellationToken cancellationToken = default)
    {
        var existing = await _dbContext.WorkstationSettings
            .FirstOrDefaultAsync(x => x.WorkstationKey == settings.WorkstationKey, cancellationToken);

        if (existing is null)
        {
            _dbContext.WorkstationSettings.Add(settings);
        }
        else
        {
            existing.DisplayName = settings.DisplayName;
            existing.DefaultPrinter = settings.DefaultPrinter;
            existing.DefaultExportFolder = settings.DefaultExportFolder;
            existing.PaperSize = settings.PaperSize;
            existing.Orientation = settings.Orientation;
            existing.IsActive = settings.IsActive;
            existing.UpdatedAt = DateTimeOffset.UtcNow;
        }

        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
