using System.Text.Json.Serialization;

namespace Merchrocket.Client.Models;

public class RequestedProduct
{
    /// <summary>
    /// The id of the catalogProduct.
    /// </summary>
    [JsonPropertyName("catalogProductId")]
    public string? CatalogProductId { get; set; }

    /// <summary>
    /// If you do not want to calculate the mockups for all variants of the product, but only for certain variants, then enter the variant IDs of the desired variants here.
    /// </summary>
    [JsonPropertyName("catalogVariantIds")]
    public List<string> CatalogVariantIds { get; set; } = [];

    [JsonPropertyName("placements")]
    public List<Placement>? Placements { get; set; }
}