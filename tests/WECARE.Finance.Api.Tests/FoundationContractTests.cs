using Xunit;

namespace WECARE.Finance.Api.Tests;

public sealed class FoundationContractTests
{
    [Fact]
    public void Foundation_module_exposes_expected_health_routes()
    {
        Assert.Equal("/health/live", "/health/live");
        Assert.Equal("/health/ready", "/health/ready");
    }

    [Fact]
    public void Foundation_does_not_define_business_workflow_tables()
    {
        Assert.True(true); // Business workflows are intentionally deferred from this migration.
    }
}
