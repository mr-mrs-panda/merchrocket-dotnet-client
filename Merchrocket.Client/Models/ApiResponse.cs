using System.Net;

namespace Merchrocket.Client.Models;

/// <summary>
/// Generic API response wrapper that bundles the deserialized response data
/// with optional <see cref="CreditInfo"/> extracted from HTTP headers.
/// On HTTP errors, <see cref="IsSuccess"/> is false and <see cref="Error"/> contains details.
/// No exceptions are thrown for non-success status codes.
/// </summary>
/// <typeparam name="T">The type of the response payload.</typeparam>
public class ApiResponse<T>
{
    /// <summary>
    /// The deserialized response body. Only valid when <see cref="IsSuccess"/> is true.
    /// </summary>
    public T? Data { get; set; }

    /// <summary>
    /// Credit information from X-MR-Credits-* response headers, if present.
    /// </summary>
    public CreditInfo? Credits { get; set; }

    /// <summary>
    /// True when the HTTP response was 2xx and deserialization succeeded.
    /// </summary>
    public bool IsSuccess => Error == null;

    /// <summary>
    /// Error details when the request failed. Null on success.
    /// </summary>
    public ApiError? Error { get; set; }

    public static ApiResponse<T> Success(T data, CreditInfo? credits = null) => new()
    {
        Data = data,
        Credits = credits
    };

    public static ApiResponse<T> Fail(HttpStatusCode statusCode, string body, string? requestPath = null) => new()
    {
        Error = new ApiError
        {
            StatusCode = statusCode,
            ResponseBody = body,
            RequestPath = requestPath
        }
    };
}

/// <summary>
/// Error details for a failed API request.
/// </summary>
public class ApiError
{
    /// <summary>HTTP status code returned by the API.</summary>
    public HttpStatusCode StatusCode { get; init; }

    /// <summary>Raw response body from the API.</summary>
    public string ResponseBody { get; init; } = string.Empty;

    /// <summary>The request path that failed.</summary>
    public string? RequestPath { get; init; }

    public override string ToString() => $"{StatusCode} on {RequestPath}: {ResponseBody}";
}
