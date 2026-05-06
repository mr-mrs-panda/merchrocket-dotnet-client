using System.Text.Json.Serialization;

namespace Merchrocket.Client.Models;

/// <summary>
/// Position and size of a design layer on a product placement.
/// Must match the placement dimensions from the catalog.
/// </summary>
public class LayerPosition
{
    /// <summary>Top edge in pixels.</summary>
    [JsonPropertyName("top")]
    public double Top { get; set; }

    /// <summary>Left edge in pixels.</summary>
    [JsonPropertyName("left")]
    public double Left { get; set; }

    /// <summary>Width in pixels.</summary>
    [JsonPropertyName("width")]
    public double Width { get; set; }

    /// <summary>Height in pixels.</summary>
    [JsonPropertyName("height")]
    public double Height { get; set; }
}
