namespace Merchrocket.Client.Models;

/// <summary>
/// Generic API response wrapper that bundles the deserialized response data
/// with optional <see cref="CreditInfo"/> extracted from HTTP headers
/// (X-MR-Credits-Included, X-MR-Credits-Used).
/// <see cref="Credits"/> is null when the endpoint does not return credit headers.
/// </summary>
/// <typeparam name="T">The type of the response payload.</typeparam>
public class ApiResponse<T>
{
    /// <summary>
    /// The deserialized response body.
    /// </summary>
    public T Data { get; set; } = default!;

    /// <summary>
    /// Credit information from X-MR-Credits-* response headers, if present.
    /// </summary>
    public CreditInfo? Credits { get; set; }
}
