namespace Merchrocket.Client.Models.Config;

/// <summary>
/// Configuration for the Merchrocket API client.
/// </summary>
public class MerchrocketConfig
{
    /// <summary>Base URL of the Merchrocket API (e.g. https://app.merchrocket.shop/api/).</summary>
    public required string BaseUrl { get; set; }

    /// <summary>Bearer access token for authentication.</summary>
    public required string AccessToken { get; set; }

    /// <summary>
    /// When true, logs every HTTP request and response via <see cref="Microsoft.Extensions.Logging.ILogger"/>.
    /// Responses with status codes >= 400 are logged at Error level.
    /// </summary>
    public bool RequestLogging { get; set; }

    /// <summary>
    /// When true (default), throws <see cref="Exceptions.MerchrocketApiException"/> on any non-success HTTP response
    /// or network error. When false, all errors are returned via <see cref="ApiResponse{T}.IsSuccess"/> = false.
    /// </summary>
    public bool ThrowOnError { get; set; } = true;
}