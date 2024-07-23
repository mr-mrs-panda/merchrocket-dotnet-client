using System.Text.Json.Serialization;

namespace Merchrocket.Client.Models;

public class Area
{
    [JsonPropertyName("width")]
    public double Width { get; set; }

    [JsonPropertyName("height")]
    public double Height { get; set; }

    [JsonPropertyName("section")]
    public Section? Section { get; set; }
}