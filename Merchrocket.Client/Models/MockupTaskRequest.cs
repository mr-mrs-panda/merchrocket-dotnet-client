using System.Text.Json.Serialization;

namespace Merchrocket.Client.Models;

public class MockupTaskRequest
{
    [JsonPropertyName("format")]
    public string? Format { get; set; }

    [JsonPropertyName("requestedProducts")]
    public List<RequestedProduct>? RequestedProducts { get; set; }
}