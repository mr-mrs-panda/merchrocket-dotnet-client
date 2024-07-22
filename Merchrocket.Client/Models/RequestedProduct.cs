using System.Text.Json.Serialization;

namespace Merchrocket.Client.Models;

public class RequestedProduct
{
    [JsonPropertyName("catalogProductId")]
    public string? CatalogProductId { get; set; }

    [JsonPropertyName("placements")]
    public List<Placement>? Placements { get; set; }
}