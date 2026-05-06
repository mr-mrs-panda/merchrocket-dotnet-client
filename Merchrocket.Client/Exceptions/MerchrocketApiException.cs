using System.Net;

namespace Merchrocket.Client.Exceptions;

/// <summary>
/// Thrown when <see cref="Models.Config.MerchrocketConfig.ThrowOnError"/> is true and the API returns
/// a non-success HTTP status code or a network error occurs.
/// </summary>
public class MerchrocketApiException : Exception
{
    /// <summary>HTTP status code returned by the API.</summary>
    public HttpStatusCode StatusCode { get; }

    /// <summary>Raw response body from the API.</summary>
    public string? ResponseBody { get; }

    /// <summary>The request path that failed.</summary>
    public string? RequestPath { get; }

    /// <summary>HTTP method of the failed request.</summary>
    public HttpMethod? HttpMethod { get; }

    public MerchrocketApiException(
        HttpStatusCode statusCode,
        string? responseBody,
        string? requestPath,
        HttpMethod? method = null)
        : base(BuildMessage(statusCode, responseBody, requestPath, method))
    {
        StatusCode = statusCode;
        ResponseBody = responseBody;
        RequestPath = requestPath;
        HttpMethod = method;
    }

    public MerchrocketApiException(string message) : base(message)
    {
    }

    public MerchrocketApiException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    private static string BuildMessage(
        HttpStatusCode statusCode, string? responseBody, string? requestPath, HttpMethod? method)
    {
        var prefix = method != null ? $"{method} " : "";
        return $"{prefix}{requestPath} returned {(int)statusCode} {statusCode}: {responseBody?.TrimTo(500)}";
    }
}

/// <summary>
/// Extension methods for string manipulation used in exception messages.
/// </summary>
file static class StringExtensions
{
    /// <summary>
    /// Trims the string to a maximum length, appending "..." if truncated.
    /// </summary>
    public static string TrimTo(this string value, int maxLength)
    {
        if (string.IsNullOrEmpty(value) || value.Length <= maxLength)
            return value ?? string.Empty;
        return value[..maxLength] + "...";
    }
}
