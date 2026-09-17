using WECAREFinance.Domain.Common;

namespace WECAREFinance.Domain.Entities;

public class AuditLog : EntityBase
{
    public string Action { get; set; } = string.Empty;
    public string EntityType { get; set; } = string.Empty;
    public Guid? EntityId { get; set; }
    public string? OldValue { get; set; }
    public string? NewValue { get; set; }
    public string? Reason { get; set; }
    public Guid? UserId { get; set; }
    public string? UserName { get; set; }
    public string? IpAddress { get; set; }
    public string? CorrelationId { get; set; }
}

public class WorkstationSettings : EntityBase
{
    public string WorkstationKey { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
    public string DefaultPrinter { get; set; } = string.Empty;
    public string DefaultExportFolder { get; set; } = string.Empty;
    public string PaperSize { get; set; } = "A4";
    public string Orientation { get; set; } = "Portrait";
    public bool IsActive { get; set; } = true;
}

public class SystemSetting : EntityBase
{
    public string Key { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool IsSensitive { get; set; }
}

public class ReferenceSequence : EntityBase
{
    public string Prefix { get; set; } = string.Empty;
    public int Year { get; set; }
    public long LastSequence { get; set; }
}
