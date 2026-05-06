using System.Text.Json.Serialization;

namespace Merchrocket.Client.Models;

public class Layer
{
    [JsonPropertyName("url")]
    public string? Url { get; set; }

    [JsonPropertyName("type")]
    public string? Type { get; set; }

    /// <summary>
    /// Position and size of the design on the placement.
    /// Required when creating orders. Must match catalog placement dimensions.
    /// </summary>
    [JsonPropertyName("position")]
    public LayerPosition? Position { get; set; }

    /// <summary>
    /// Layer-level options for order creation.
    /// Always serialized as array (never null).
    /// </summary>
    [JsonPropertyName("layerOptions")]
    public List<PlacementOption> LayerOptions { get; set; } = [];
}