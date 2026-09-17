using Microsoft.Extensions.Diagnostics.HealthChecks;
using WECAREFinance.Domain.Entities;

namespace WECAREFinance.Infrastructure.Services;

public class PostgresHealthCheck : IHealthCheck
{
    private readonly AppDbContext _dbContext;

    public PostgresHealthCheck(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        try
        {
            var canConnect = _dbContext.Database.CanConnect();
            return Task.FromResult(canConnect
                ? HealthCheckResult.Healthy("PostgreSQL connection available.")
                : HealthCheckResult.Unhealthy("PostgreSQL connection failed."));
        }
        catch (Exception ex)
        {
            return Task.FromResult(HealthCheckResult.Unhealthy("PostgreSQL error", ex));
        }
    }
}
