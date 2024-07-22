using System.Text.Json.Serialization;

namespace Merchrocket.Client.Models.Hydra;

public class HydraView
{
    [JsonPropertyName("@id")]
    public string? Id { get; set; }

    [JsonPropertyName("@type")]
    public string? Type { get; set; }

    [JsonPropertyName("hydra:first")]
    public string? First { get; set; }

    [JsonPropertyName("hydra:last")]
    public string? Last { get; set; }

    [JsonPropertyName("hydra:next")]
    public string? Next { get; set; }
}