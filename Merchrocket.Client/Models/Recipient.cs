using System.Text.Json.Serialization;

namespace Merchrocket.Client.Models;

/// <summary>
/// Shipping address / recipient for an order.
/// </summary>
public class Recipient
{
    /// <summary>Name of the recipient. Required.</summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    /// <summary>Company name (optional).</summary>
    [JsonPropertyName("company")]
    public string? Company { get; set; }

    /// <summary>First address line.</summary>
    [JsonPropertyName("address1")]
    public string? Address1 { get; set; }

    /// <summary>Second address line (optional).</summary>
    [JsonPropertyName("address2")]
    public string? Address2 { get; set; }

    /// <summary>City.</summary>
    [JsonPropertyName("city")]
    public string? City { get; set; }

    /// <summary>State or province code.</summary>
    [JsonPropertyName("stateCode")]
    public string? StateCode { get; set; }

    /// <summary>State or province name.</summary>
    [JsonPropertyName("stateName")]
    public string? StateName { get; set; }

    /// <summary>ISO 3166-1 alpha-2 country code.</summary>
    [JsonPropertyName("countryCode")]
    public string? CountryCode { get; set; }

    /// <summary>Country name.</summary>
    [JsonPropertyName("countryName")]
    public string? CountryName { get; set; }

    /// <summary>Postal code.</summary>
    [JsonPropertyName("zip")]
    public string? Zip { get; set; }

    /// <summary>Phone number.</summary>
    [JsonPropertyName("phone")]
    public string? Phone { get; set; }

    /// <summary>Email address.</summary>
    [JsonPropertyName("email")]
    public string? Email { get; set; }

    /// <summary>Tax identification number.</summary>
    [JsonPropertyName("taxNumber")]
    public string? TaxNumber { get; set; }
}
