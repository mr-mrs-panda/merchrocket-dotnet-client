using System.Text.Json.Serialization;

namespace Merchrocket.Client.Models;

public class Area
{
    [JsonPropertyName("width")]
    public int Width { get; set; }

    [JsonPropertyName("height")]
    public int Height { get; set; }

    [JsonPropertyName("section")]
    public Section? Section { get; set; }
}