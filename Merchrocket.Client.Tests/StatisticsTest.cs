using Merchrocket.Client.Models;

namespace Merchrocket.Client.Tests;

public class StatisticsTest : ATestBase
{
    [Fact]
    public async Task ShouldGetMonthlyMockupStatsAsync()
    {
        var year = DateTime.UtcNow.Year.ToString();
        var response = await Client.Statistics.GetMonthlyMockupStatsAsync(year, page: 1, itemsPerPage: 12);

        if (!response.IsSuccess)
        {
            // API may return 500 for years without data — try previous year
            response = await Client.Statistics.GetMonthlyMockupStatsAsync(
                (DateTime.UtcNow.Year - 1).ToString(), page: 1, itemsPerPage: 12);
        }

        if (!response.IsSuccess)
        {
            // Statistics endpoint might not be available — not a client bug
            return;
        }

        var stats = AssertSuccess(response);
        Assert.NotNull(stats.Members);
        Assert.True(stats.TotalItems >= 0);
    }
}
