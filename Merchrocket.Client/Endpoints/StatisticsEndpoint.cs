using Merchrocket.Client.Models;
using Merchrocket.Client.Models.Hydra;

namespace Merchrocket.Client.Endpoints;

public interface IStatisticsEndpoint
{
    /// <summary>
    /// Retrieves monthly mockup statistics for a given year.
    /// GET /api/statistics/mockups/{year}
    /// </summary>
    Task<ApiResponse<HydraCollection<StatisticsMockupMonthResult>>> GetMonthlyMockupStatsAsync(string year, int page = 1, int itemsPerPage = 30);
}

public class StatisticsEndpoint(IHydraClient client) : IStatisticsEndpoint
{
    public async Task<ApiResponse<HydraCollection<StatisticsMockupMonthResult>>> GetMonthlyMockupStatsAsync(string year, int page = 1, int itemsPerPage = 30)
        => await client.GetCollectionAsync<StatisticsMockupMonthResult>($"statistics/mockups/{year}", page, itemsPerPage);
}
