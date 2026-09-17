using WECAREFinance.Domain.Entities;

namespace WECAREFinance.Application.Abstractions;

public interface IReferenceNumberService
{
    string Generate(string prefix, DateTimeOffset? timestamp = null);
}

public interface IAuditService
{
    Task WriteAsync(AuditLog auditLog, CancellationToken cancellationToken = default);
}

public interface IWorkstationSettingsService
{
    Task<WorkstationSettings> GetAsync(string workstationKey, CancellationToken cancellationToken = default);
    Task SaveAsync(WorkstationSettings settings, CancellationToken cancellationToken = default);
}
