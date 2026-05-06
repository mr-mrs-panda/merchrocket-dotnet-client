using System.Text.Json.Serialization;

namespace Merchrocket.Client.Models;

/// <summary>
/// Usage statistics included in webhook payloads (e.g. mockup_task.finished).
/// </summary>
public class Statistics
{
    /// <summary>
    /// Total count of credit-relevant actions for the current calendar month.
    /// </summary>
    [JsonPropertyName("countPerMonth")]
    public int CountPerMonth { get; set; }
}
