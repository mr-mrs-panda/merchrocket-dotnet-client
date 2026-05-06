using System.Text.Json;
using System.Text.Json.Serialization;
using Merchrocket.Client.Models.Hydra;

namespace Merchrocket.Client.Models;

/// <summary>
/// Represents a Merchrocket order (response).
/// </summary>
public class Order : HydraMember
{
    /// <summary>Internal Merchrocket order ID.</summary>
    [JsonPropertyName("id")]
    public string? OrderId { get; set; }

    /// <summary>
    /// External ID linking this order to your system.
    /// Max 63 chars: letters, numbers, hyphens, underscores.
    /// </summary>
    [JsonPropertyName("externalId")]
    public string? ExternalId { get; set; }

    /// <summary>Shipping method (e.g. STANDARD, DHL_EXPRESS).</summary>
    [JsonPropertyName("shipping")]
    public string? Shipping { get; set; }

    /// <summary>Estimated delivery date.</summary>
    [JsonPropertyName("estimatedDeliveryDate")]
    public string? EstimatedDeliveryDate { get; set; }

    /// <summary>Current order status.</summary>
    [JsonPropertyName("status")]
    public string? Status { get; set; }

    /// <summary>Recipient / shipping address.</summary>
    [JsonPropertyName("recipient")]
    public Recipient? Recipient { get; set; }

    /// <summary>Items in the order.</summary>
    [JsonPropertyName("orderItems")]
    public List<OrderItem>? OrderItems { get; set; }

    /// <summary>Shipments associated with this order.</summary>
    [JsonPropertyName("shipments")]
    public List<ShipmentInfo>? Shipments { get; set; }

    /// <summary>Order-level errors.</summary>
    [JsonPropertyName("errors")]
    public JsonElement? Errors { get; set; }

    /// <summary>ISO 8601 creation timestamp.</summary>
    [JsonPropertyName("createdAt")]
    public DateTime? CreatedAt { get; set; }

    /// <summary>ISO 8601 last-update timestamp.</summary>
    [JsonPropertyName("updatedAt")]
    public DateTime? UpdatedAt { get; set; }

    /// <summary>Custom metadata dictionary.</summary>
    [JsonPropertyName("metadata")]
    public Dictionary<string, object>? Metadata { get; set; }

    /// <summary>Order associations (reprint, refund relationships).</summary>
    [JsonPropertyName("orderAssociations")]
    public List<OrderAssociation>? OrderAssociations { get; set; }
}

/// <summary>
/// Represents an item within a Merchrocket order.
/// </summary>
public class OrderItem : HydraMember
{
    /// <summary>Internal Merchrocket item ID.</summary>
    [JsonPropertyName("id")]
    public string? ItemId { get; set; }

    /// <summary>Your external identifier for this item.</summary>
    [JsonPropertyName("externalId")]
    public string? ExternalId { get; set; }

    /// <summary>Quantity of this item.</summary>
    [JsonPropertyName("quantity")]
    public int Quantity { get; set; }

    /// <summary>Name of the item (freely assignable).</summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    /// <summary>Your retail price as "currency price" string.</summary>
    [JsonPropertyName("retailPrice")]
    public string? RetailPrice { get; set; }

    /// <summary>Catalog variant ID to produce.</summary>
    [JsonPropertyName("variantId")]
    public string? VariantId { get; set; }

    /// <summary>Print placement definitions.</summary>
    [JsonPropertyName("placements")]
    public List<Placement>? Placements { get; set; }

    /// <summary>Product options for this item.</summary>
    [JsonPropertyName("options")]
    public List<Option>? Options { get; set; }

    /// <summary>Item-level errors.</summary>
    [JsonPropertyName("errors")]
    public JsonElement? Errors { get; set; }

    /// <summary>Generated item images.</summary>
    [JsonPropertyName("images")]
    public JsonElement? Images { get; set; }

    /// <summary>Internal metadata (Merchrocket-side).</summary>
    [JsonPropertyName("metadataInternal")]
    public JsonElement? MetadataInternal { get; set; }

    /// <summary>Custom metadata dictionary.</summary>
    [JsonPropertyName("metadata")]
    public Dictionary<string, object>? Metadata { get; set; }
}

/// <summary>
/// Shipment information for an order.
/// </summary>
public class ShipmentInfo : HydraMember
{
    /// <summary>Unique shipment identifier.</summary>
    [JsonPropertyName("id")]
    public string? ShipmentId { get; set; }

    /// <summary>Carrier name (DHL, UPS, GLS).</summary>
    [JsonPropertyName("carrier")]
    public string? Carrier { get; set; }

    /// <summary>Shipping service level (STANDARD, DHL_EXPRESS).</summary>
    [JsonPropertyName("service")]
    public string? Service { get; set; }

    /// <summary>Shipment method details from the API.</summary>
    [JsonPropertyName("shipmentMethod")]
    public JsonElement? ShipmentMethod { get; set; }

    /// <summary>Current shipment status.</summary>
    [JsonPropertyName("shipmentStatus")]
    public string? ShipmentStatus { get; set; }

    /// <summary>When the shipment was dispatched.</summary>
    [JsonPropertyName("shippedAt")]
    public DateTime? ShippedAt { get; set; }

    /// <summary>Tracking URL.</summary>
    [JsonPropertyName("trackingUrl")]
    public string? TrackingUrl { get; set; }

    /// <summary>Tracking ID.</summary>
    [JsonPropertyName("trackingId")]
    public string? TrackingId { get; set; }
}

/// <summary>
/// Represents a relationship between orders (reprint, refund).
/// </summary>
public class OrderAssociation
{
    /// <summary>Type of association ("reprint" or "refund").</summary>
    [JsonPropertyName("type")]
    public string? Type { get; set; }

    /// <summary>When the association was created.</summary>
    [JsonPropertyName("createdAt")]
    public DateTime? CreatedAt { get; set; }

    /// <summary>The source order.</summary>
    [JsonPropertyName("source")]
    public OrderReference? Source { get; set; }

    /// <summary>Linked orders.</summary>
    [JsonPropertyName("linked")]
    public List<OrderReference>? Linked { get; set; }
}

/// <summary>
/// Lightweight reference to an order used in associations.
/// </summary>
public class OrderReference
{
    [JsonPropertyName("@id")]
    public string? Id { get; set; }

    [JsonPropertyName("@type")]
    public string? Type { get; set; }

    [JsonPropertyName("id")]
    public string? OrderId { get; set; }

    [JsonPropertyName("externalId")]
    public string? ExternalId { get; set; }
}
