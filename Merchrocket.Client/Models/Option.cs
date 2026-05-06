using System.Text.Json.Serialization;

namespace Merchrocket.Client.Models;

/// <summary>
/// A product option required by certain product variants (e.g. color_scheme).
/// </summary>
public class Option
{
    /// <summary>ID of the product option.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    /// <summary>Value of the product option.</summary>
    [JsonPropertyName("value")]
    public string? Value { get; set; }
}

/// <summary>
/// A placement-level option (e.g. unlimited_color).
/// </summary>
public class PlacementOption
{
    /// <summary>Name of the placement option.</summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    /// <summary>Value of the placement option.</summary>
    [JsonPropertyName("value")]
    public bool Value { get; set; }
}
