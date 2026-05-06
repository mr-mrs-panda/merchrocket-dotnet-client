using System.Text.Json.Serialization;

namespace Merchrocket.Client.Models;

public class Placement
{
    [JsonPropertyName("type")]
    public string? Type { get; set; }

    [JsonPropertyName("technique")]
    public string? Technique { get; set; }

    [JsonPropertyName("area")]
    public Area? Area { get; set; }
    
    [JsonPropertyName("layers")]
    public List<Layer> Layers { get; set; } = [];

    /// <summary>
    /// Placement-level options for order creation (e.g. unlimited_color).
    /// Always serialized as array (never null).
    /// </summary>
    [JsonPropertyName("placementOptions")]
    public List<PlacementOption> PlacementOptions { get; set; } = [];

    /// <summary>
    /// Whether the placement is required (catalog response only).
    /// </summary>
    [JsonPropertyName("required")]
    public bool? Required { get; set; }

    /// <summary>
    /// Available techniques for this placement (catalog response only).
    /// </summary>
    [JsonPropertyName("techniques")]
    public List<string>? Techniques { get; set; }

    /// <summary>
    /// Visible area of the placement (catalog response only).
    /// </summary>
    [JsonPropertyName("visibleArea")]
    public VisibleArea? VisibleArea { get; set; }
}

/// <summary>
/// Visible area of a placement (catalog response).
/// </summary>
public class VisibleArea
{
    [JsonPropertyName("top")]
    public double Top { get; set; }

    [JsonPropertyName("left")]
    public double Left { get; set; }

    [JsonPropertyName("width")]
    public double Width { get; set; }

    [JsonPropertyName("height")]
    public double Height { get; set; }
}