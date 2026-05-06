using System.Text.Json.Serialization;

namespace Merchrocket.Client.Models;

/// <summary>
/// Request payload for creating a new Merchrocket order.
/// </summary>
public class CreateOrderRequest
{
    /// <summary>
    /// External ID linking this order to your system.
    /// Max 63 chars: letters, numbers, hyphens, underscores. Required.
    /// </summary>
    [JsonPropertyName("externalId")]
    public string? ExternalId { get; set; }

    /// <summary>Shipping method: STANDARD or DHL_EXPRESS.</summary>
    [JsonPropertyName("shipping")]
    public string? Shipping { get; set; }

    /// <summary>Shipping address / recipient. Required.</summary>
    [JsonPropertyName("recipient")]
    public Recipient? Recipient { get; set; }

    /// <summary>Items in the order. Required.</summary>
    [JsonPropertyName("orderItems")]
    public List<CreateOrderItemRequest>? OrderItems { get; set; }

    /// <summary>Custom metadata dictionary (e.g. CUST_ORDERID_EXTERN).</summary>
    [JsonPropertyName("metadata")]
    public Dictionary<string, object>? Metadata { get; set; }

    /// <summary>
    /// Actions to perform on order creation (e.g. reprint).
    /// </summary>
    [JsonPropertyName("actions")]
    public List<OrderAction>? Actions { get; set; }
}

/// <summary>
/// Request payload for an order item within a create order request.
/// </summary>
public class CreateOrderItemRequest
{
    /// <summary>Your external identifier for this item.</summary>
    [JsonPropertyName("externalId")]
    public string? ExternalId { get; set; }

    /// <summary>Quantity. Required.</summary>
    [JsonPropertyName("quantity")]
    public int Quantity { get; set; }

    /// <summary>Name of the item (freely assignable).</summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    /// <summary>Your retail price as "currency price" string.</summary>
    [JsonPropertyName("retailPrice")]
    public string? RetailPrice { get; set; }

    /// <summary>Catalog variant ID to produce. Required.</summary>
    [JsonPropertyName("variantId")]
    public string? VariantId { get; set; }

    /// <summary>Print placement definitions.</summary>
    [JsonPropertyName("placements")]
    public List<Placement>? Placements { get; set; }

    /// <summary>Product options for this item. Always serialized as array.</summary>
    [JsonPropertyName("options")]
    public List<Option> Options { get; set; } = [];

    /// <summary>Custom metadata dictionary (e.g. DP_ITEMID_EXTERN, CUST_PRODDATA6).</summary>
    [JsonPropertyName("metadata")]
    public Dictionary<string, object>? Metadata { get; set; }
}

/// <summary>
/// An action performed during or on an order (e.g. reprint).
/// </summary>
public class OrderAction
{
    /// <summary>Action type (currently "reprint" is the only option).</summary>
    [JsonPropertyName("type")]
    public string? Type { get; set; }

    /// <summary>
    /// Source order external ID prefixed with @ (e.g. "@TCS-5442223218").
    /// </summary>
    [JsonPropertyName("source")]
    public string? Source { get; set; }
}

/// <summary>
/// Request payload for patching an existing order.
/// Only populated properties will be updated (JSON merge patch).
/// </summary>
public class PatchOrderRequest
{
    /// <summary>Updated shipping method.</summary>
    [JsonPropertyName("shipping")]
    public string? Shipping { get; set; }

    /// <summary>Updated recipient.</summary>
    [JsonPropertyName("recipient")]
    public Recipient? Recipient { get; set; }

    /// <summary>Updated metadata.</summary>
    [JsonPropertyName("metadata")]
    public Dictionary<string, object>? Metadata { get; set; }
}
