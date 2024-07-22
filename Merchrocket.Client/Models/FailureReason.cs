using System.Text.Json.Serialization;
using Merchrocket.Client.Models.Hydra;

namespace Merchrocket.Client.Models;

public class FailureReason: HydraMember
{
    [JsonPropertyName("type")]
    public string? FailureType { get; set; }

    [JsonPropertyName("detail")]
    public string? Details { get; set; }
    
}