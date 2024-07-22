using System.Text.Json.Serialization;

namespace Merchrocket.Client.Models;

public class Layer
{
    [JsonPropertyName("url")]
    public string? Url { get; set; }

    [JsonPropertyName("type")]
    public string? Type { get; set; }
}