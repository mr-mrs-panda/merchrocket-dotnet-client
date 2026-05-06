using System.Text.Json.Serialization;
using Merchrocket.Client.Models.Hydra;

namespace Merchrocket.Client.Models;

/// <summary>
/// User account in the Merchrocket system.
/// </summary>
public class User : HydraMember
{
    /// <summary>Internal user ID.</summary>
    [JsonPropertyName("id")]
    public string? UserId { get; set; }

    /// <summary>Email address.</summary>
    [JsonPropertyName("email")]
    public string? Email { get; set; }

    /// <summary>Display name.</summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    /// <summary>Phone number.</summary>
    [JsonPropertyName("telephone")]
    public string? Telephone { get; set; }

    /// <summary>Locale code (e.g. de_DE, en_US).</summary>
    [JsonPropertyName("localeCode")]
    public string? LocaleCode { get; set; }

    /// <summary>Roles assigned to this user.</summary>
    [JsonPropertyName("roles")]
    public List<string>? Roles { get; set; }

    /// <summary>When the user last logged in.</summary>
    [JsonPropertyName("loggedInAt")]
    public DateTime? LoggedInAt { get; set; }

    /// <summary>ISO 8601 creation timestamp.</summary>
    [JsonPropertyName("createdAt")]
    public DateTime? CreatedAt { get; set; }

    /// <summary>ISO 8601 last-update timestamp.</summary>
    [JsonPropertyName("updatedAt")]
    public DateTime? UpdatedAt { get; set; }

    /// <summary>When the user was invited.</summary>
    [JsonPropertyName("invitedAt")]
    public DateTime? InvitedAt { get; set; }

    /// <summary>When the invitation expires.</summary>
    [JsonPropertyName("invitationExpiresAt")]
    public DateTime? InvitationExpiresAt { get; set; }

    /// <summary>Whether the user is currently online.</summary>
    [JsonPropertyName("isOnline")]
    public bool IsOnline { get; set; }
}
