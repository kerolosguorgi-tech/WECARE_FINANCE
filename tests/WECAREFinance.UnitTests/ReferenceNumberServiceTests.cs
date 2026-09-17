using Xunit;

namespace WECAREFinance.UnitTests;

public class ReferenceNumberServiceTests
{
    [Fact]
    public void Generate_ShouldIncludePrefix_AndYear_AndSequentialPattern()
    {
        var service = new WECAREFinance.Infrastructure.Services.ReferenceNumberService();
        var code = service.Generate("INV", DateTimeOffset.Parse("2026-09-17T00:00:00+00:00"));

        Assert.StartsWith("INV-2026-", code);
        Assert.True(code.Length > "INV-2026-".Length);
        Assert.Matches(@"^INV-2026-\d{6}$", code);
    }
}
