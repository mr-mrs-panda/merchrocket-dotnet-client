using System.Text.Json.Serialization;
using Merchrocket.Client.Models.Hydra;

namespace Merchrocket.Client.Models;

public class CatalogProductVariant : HydraMember
{
    [JsonPropertyName("id")] public string? VariantId { get; set; }

    [JsonPropertyName("sku")] public string? Sku { get; set; }

    [JsonPropertyName("name")] public string? Name { get; set; }

    [JsonPropertyName("image")] public string? Image { get; set; }

    [JsonPropertyName("placements")] public List<Placement>? Placements { get; set; }

    [JsonPropertyName("attributes")] public List<Attribute>? Attributes { get; set; }

    public T? TryGetAttributeValue<T>(string code)
    {
        var attribute = Attributes?.FirstOrDefault(a => a.Code == code);
        return attribute == default ? default : attribute.GetValue<T>();
    }
}