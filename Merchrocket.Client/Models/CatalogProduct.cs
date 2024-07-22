using System.Text.Json.Serialization;
using Merchrocket.Client.Models.Hydra;

namespace Merchrocket.Client.Models;

public class CatalogProduct: HydraMember
{
    [JsonPropertyName("id")]
    public string? ProductId { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("brand")]
    public string? Brand { get; set; }

    [JsonPropertyName("image")]
    public string? Image { get; set; }

    [JsonPropertyName("variantCount")]
    public int VariantCount { get; set; }

    [JsonPropertyName("variants")]
    public string? Variants { get; set; }
}