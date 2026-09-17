namespace WECAREFinance.Application.Authorization;

public static class Permissions
{
    public const string SystemView = "System.View";
    public const string SystemConfigure = "System.Configure";
    public const string AuditView = "Audit.View";
    public const string SensitiveDataView = "SensitiveData.View";
    public const string Override = "System.Override";
}

public interface ICurrentUser
{
    Guid? UserId { get; }
    string? UserName { get; }
    bool IsAuthenticated { get; }
    bool IsPowerAdmin { get; }
    bool HasPermission(string permission);
}

public interface IPermissionChecker
{
    void Demand(string permission);
}
