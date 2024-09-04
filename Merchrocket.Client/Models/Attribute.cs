using System.Text.Json;
using System.Text.Json.Serialization;

namespace Merchrocket.Client.Models;

public class Attribute
{
    [JsonPropertyName("code")] public string? Code { get; set; }

    [JsonPropertyName("name")] public string? Name { get; set; }

    [JsonPropertyName("value")] public JsonDocument? Value { get; set; }

    [JsonPropertyName("type")] public string? Type { get; set; }

    public T? GetValue<T>()
    {
        return Value == null ? default : Value.RootElement.Deserialize<T>();
    }
}