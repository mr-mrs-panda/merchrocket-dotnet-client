using System.Text.Json.Serialization;
using Merchrocket.Client.Models.Hydra;

namespace Merchrocket.Client.Models;

public class MockupTask: HydraMember
{
    [JsonPropertyName("id")]
    public string? MockupTaskId { get; set; }

    [JsonPropertyName("status")]
    public string? Status { get; set; }

    [JsonPropertyName("catalogVariantMockups")]
    public List<CatalogVariantMockup>? CatalogVariantMockups { get; set; }

    [JsonPropertyName("failureReasons")]
    public List<FailureReason>? FailureReasons { get; set; }

    [JsonPropertyName("createdAt")]
    public DateTime? CreatedAt { get; set; }

    [JsonPropertyName("finishedAt")]
    public DateTime? FinishedAt { get; set; }
}

public class CatalogVariantMockup: HydraMember
{
    [JsonPropertyName("catalogVariantId")]
    public string? CatalogVariantId { get; set; }

    [JsonPropertyName("mockups")]
    public List<Mockup>? Mockups { get; set; }
}

public class Mockup: HydraMember
{
    [JsonPropertyName("placement")]
    public string? Placement { get; set; }

    [JsonPropertyName("displayName")]
    public string? DisplayName { get; set; }

    [JsonPropertyName("technique")]
    public string? Technique { get; set; }

    [JsonPropertyName("mockupUrl")]
    public string? MockupUrl { get; set; }
}