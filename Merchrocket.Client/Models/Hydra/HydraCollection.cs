using System.Text.Json.Serialization;

namespace Merchrocket.Client.Models.Hydra;

public class HydraCollection<T> where T: HydraMember
{
    [JsonPropertyName("@context")]
    public string? Context { get; set; }

    [JsonPropertyName("@id")]
    public string? Id { get; set; }

    [JsonPropertyName("@type")]
    public string? Type { get; set; }

    [JsonPropertyName("hydra:totalItems")]
    public int TotalItems { get; set; }

    [JsonPropertyName("hydra:member")]
    public List<T>? Members { get; set; }

    [JsonPropertyName("hydra:view")]
    public HydraView? View { get; set; }
}