using System.Text.Json.Serialization;

namespace Merchrocket.Client.Models;

/// <summary>
/// Detailed product option definition from the catalog (e.g. color choices, dial types).
/// </summary>
public class ProductOptionDefinition
{
    /// <summary>Option identifier (e.g. "dial_color", "color_scheme").</summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    /// <summary>Data type of the option value.</summary>
    [JsonPropertyName("type")]
    public string? Type { get; set; }

    /// <summary>Human-readable label.</summary>
    [JsonPropertyName("label")]
    public string? Label { get; set; }

    /// <summary>Whether this option must be provided when ordering.</summary>
    [JsonPropertyName("required")]
    public bool Required { get; set; }

    /// <summary>Available values for this option.</summary>
    [JsonPropertyName("values")]
    public List<ProductOptionValue>? Values { get; set; }
}

/// <summary>
/// A possible value for a product option.
/// </summary>
public class ProductOptionValue
{
    /// <summary>The option value to submit (e.g. "black", "light_blue").</summary>
    [JsonPropertyName("value")]
    public string? Value { get; set; }

    /// <summary>Preview image for this option value.</summary>
    [JsonPropertyName("image")]
    public ProductOptionImage? Image { get; set; }
}

/// <summary>
/// Preview image for a product option value.
/// </summary>
public class ProductOptionImage
{
    /// <summary>Image URL.</summary>
    [JsonPropertyName("url")]
    public string? Url { get; set; }

    /// <summary>Alt text for the image.</summary>
    [JsonPropertyName("alt")]
    public string? Alt { get; set; }
}
