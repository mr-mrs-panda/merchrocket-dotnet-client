using System.Text.Json.Serialization;

namespace Merchrocket.Client.Models.Hydra;

public class HydraMember
{
    [JsonPropertyName("@id")]
    public string? Id { get; set; }

    [JsonPropertyName("@type")]
    public string? Type { get; set; }

    [JsonPropertyName("@context")]
    public string? Context { get; set; }
}