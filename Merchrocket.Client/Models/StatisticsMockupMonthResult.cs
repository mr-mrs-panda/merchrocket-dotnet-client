using System.Text.Json.Serialization;
using Merchrocket.Client.Models.Hydra;

namespace Merchrocket.Client.Models;

/// <summary>
/// Monthly mockup generation statistics.
/// Read-only.
/// </summary>
public class StatisticsMockupMonthResult : HydraMember
{
    /// <summary>Number of mockups generated.</summary>
    [JsonPropertyName("count")]
    public int Count { get; set; }

    /// <summary>Type of mockup statistic.</summary>
    [JsonPropertyName("type")]
    public string? StatisticType { get; set; }

    /// <summary>Year (e.g. "2026").</summary>
    [JsonPropertyName("year")]
    public string? Year { get; set; }

    /// <summary>Month (1-12).</summary>
    [JsonPropertyName("month")]
    public string? Month { get; set; }
}
