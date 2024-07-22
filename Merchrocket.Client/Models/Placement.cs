using System.Text.Json.Serialization;

namespace Merchrocket.Client.Models;

public class Placement
{
    [JsonPropertyName("type")]
    public string? Type { get; set; }

    [JsonPropertyName("technique")]
    public string? Technique { get; set; }

    [JsonPropertyName("area")]
    public Area? Area { get; set; }
}