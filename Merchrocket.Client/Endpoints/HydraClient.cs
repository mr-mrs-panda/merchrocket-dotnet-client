using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Merchrocket.Client.Exceptions;
using Merchrocket.Client.Models;
using Merchrocket.Client.Models.Config;
using Merchrocket.Client.Models.Hydra;
using Microsoft.Extensions.Logging;

namespace Merchrocket.Client.Endpoints;

public interface IHydraClient
{
    Task<ApiResponse<HydraCollection<T>>> GetCollectionAsync<T>(
        string path, int page = 1, int itemsPerPage = 30,
        Dictionary<string, string>? queryParams = null) where T : HydraMember;

    Task<ApiResponse<T>> GetAsync<T>(string path) where T : HydraMember;
    Task<ApiResponse<List<T>>> GetListAsync<T>(string path) where T : class;
    Task<ApiResponse<object>> DeleteAsync(string path);
    Task<ApiResponse<byte[]>> GetBytesAsync(string path);

    Task<ApiResponse<TResponse>> PostAsync<TResponse, TRequest>(string path, TRequest request)
        where TResponse : HydraMember;

    Task<ApiResponse<TResponse>> PostActionAsync<TResponse>(string path)
        where TResponse : HydraMember;

    Task<ApiResponse<TResponse>> PatchAsync<TResponse, TRequest>(string path, TRequest request)
        where TResponse : HydraMember;
}

public class HydraClient(MerchrocketConfig config, ILogger<HydraClient> logger) : IHydraClient
{
    private static readonly JsonSerializerOptions JsonOptions = new() { PropertyNameCaseInsensitive = true };

    public async Task<ApiResponse<HydraCollection<T>>> GetCollectionAsync<T>(
        string path, int page = 1, int itemsPerPage = 30,
        Dictionary<string, string>? queryParams = null) where T : HydraMember
    {
        var qp = new Dictionary<string, string> { { "page", page.ToString() }, { "itemsPerPage", itemsPerPage.ToString() } };
        if (queryParams != null)
            foreach (var (k, v) in queryParams) qp[k] = v;
        var qs = string.Join("&", qp.Select(kvp => $"{Uri.EscapeDataString(kvp.Key)}={Uri.EscapeDataString(kvp.Value)}"));
        return await SendAsync<HydraCollection<T>>(HttpMethod.Get, $"{path}?{qs}");
    }

    public async Task<ApiResponse<T>> GetAsync<T>(string path) where T : HydraMember
        => await SendAsync<T>(HttpMethod.Get, path);

    public async Task<ApiResponse<List<T>>> GetListAsync<T>(string path) where T : class
        => await SendAsync<List<T>>(HttpMethod.Get, path);

    public async Task<ApiResponse<byte[]>> GetBytesAsync(string path)
        => await SendBytesAsync(path);

    public async Task<ApiResponse<object>> DeleteAsync(string path)
        => await SendAsync<object>(HttpMethod.Delete, path);

    public async Task<ApiResponse<TResponse>> PostAsync<TResponse, TRequest>(string path, TRequest request)
        where TResponse : HydraMember
        => await SendAsync<TResponse>(HttpMethod.Post, path, JsonContent.Create(request));

    public async Task<ApiResponse<TResponse>> PostActionAsync<TResponse>(string path)
        where TResponse : HydraMember
        => await SendAsync<TResponse>(HttpMethod.Post, path);

    public async Task<ApiResponse<TResponse>> PatchAsync<TResponse, TRequest>(string path, TRequest request)
        where TResponse : HydraMember
    {
        var content = JsonContent.Create(request, mediaType: new System.Net.Http.Headers.MediaTypeHeaderValue("application/merge-patch+json"));
        return await SendAsync<TResponse>(HttpMethod.Patch, path, content);
    }

    // --- core send logic ---

    private async Task<ApiResponse<T>> SendAsync<T>(HttpMethod method, string path, HttpContent? content = null)
    {
        using var client = CreateClient();
        var url = $"{config.BaseUrl.TrimEnd('/')}/{path.TrimStart('/')}";

        try
        {
            LogRequest(method, path);
            var request = new HttpRequestMessage(method, url) { Content = content };
            var response = await client.SendAsync(request);
            var body = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                LogHttpError(response.StatusCode, path, method, body);
                if (config.ThrowOnError)
                    throw new MerchrocketApiException(response.StatusCode, body, path, method);
                return ApiResponse<T>.Fail(response.StatusCode, body, path);
            }

            LogSuccess(response.StatusCode, path);
            var credits = ExtractCreditInfo(response);

            if (typeof(T) == typeof(object) && string.IsNullOrWhiteSpace(body))
                return ApiResponse<T>.Success(default!);

            var data = JsonSerializer.Deserialize<T>(body, JsonOptions)
                       ?? throw new InvalidOperationException("Deserialized null");
            return ApiResponse<T>.Success(data, credits);
        }
        catch (Exception ex) when (ex is not InvalidOperationException and not MerchrocketApiException)
        {
            logger.LogError(ex, "Merchrocket.Client | {Method} request failed: {Path}", method, path);
            if (config.ThrowOnError)
                throw new MerchrocketApiException($"{method} {path} failed: {ex.Message}", ex);
            return ApiResponse<T>.Fail(HttpStatusCode.InternalServerError, ex.Message, path);
        }
    }

    private async Task<ApiResponse<byte[]>> SendBytesAsync(string path)
    {
        using var client = CreateClient();
        var url = $"{config.BaseUrl.TrimEnd('/')}/{path.TrimStart('/')}";

        try
        {
            LogRequest(HttpMethod.Get, path);
            var response = await client.GetAsync(url);
            var body = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                LogHttpError(response.StatusCode, path, HttpMethod.Get, body);
                if (config.ThrowOnError)
                    throw new MerchrocketApiException(response.StatusCode, body, path, HttpMethod.Get);
                return ApiResponse<byte[]>.Fail(response.StatusCode, body, path);
            }

            LogSuccess(response.StatusCode, path);
            var bytes = await response.Content.ReadAsByteArrayAsync();
            return ApiResponse<byte[]>.Success(bytes, ExtractCreditInfo(response));
        }
        catch (Exception ex) when (ex is not MerchrocketApiException)
        {
            logger.LogError(ex, "Merchrocket.Client | GET request failed: {Path}", path);
            if (config.ThrowOnError)
                throw new MerchrocketApiException($"GET {path} failed: {ex.Message}", ex);
            return ApiResponse<byte[]>.Fail(HttpStatusCode.InternalServerError, ex.Message, path);
        }
    }

    // --- logging ---

    /// <summary>
    /// Logs the outgoing request. Uses Debug level by default, Information when <see cref="MerchrocketConfig.RequestLogging"/> is enabled.
    /// </summary>
    private void LogRequest(HttpMethod method, string path)
    {
        logger.LogDebug("Merchrocket.Client | -> {Method} {Path}", method, path);
        if (config.RequestLogging)
            logger.LogInformation("Merchrocket.Client | -> {Method} {Path}", method, path);
    }

    /// <summary>
    /// Logs a successful response. Only logged when <see cref="MerchrocketConfig.RequestLogging"/> is enabled.
    /// </summary>
    private void LogSuccess(HttpStatusCode statusCode, string path)
    {
        if (config.RequestLogging)
            logger.LogInformation("Merchrocket.Client | <- {(int)statusCode} {Path}", (int)statusCode, path);
    }

    /// <summary>
    /// Logs an HTTP error response (4xx/5xx). Always logged at Warning level, regardless of <see cref="MerchrocketConfig.RequestLogging"/>.
    /// </summary>
    private void LogHttpError(HttpStatusCode statusCode, string path, HttpMethod method, string body)
    {
        logger.LogWarning("Merchrocket.Client | <- {(int)statusCode} {Method} {Path}: {Body}", (int)statusCode, method, path, body);
    }

    // --- helpers ---

    private static CreditInfo? ExtractCreditInfo(HttpResponseMessage response)
    {
        response.Headers.TryGetValues("X-MR-Credits-Included", out var inc);
        response.Headers.TryGetValues("X-MR-Credits-Used", out var used);
        var included = inc?.FirstOrDefault();
        var usedVal = used?.FirstOrDefault();
        if (included == null && usedVal == null) return null;
        return new CreditInfo
        {
            CreditsIncluded = int.TryParse(included, out var i) ? i : null,
            CreditsUsed = int.TryParse(usedVal, out var u) ? u : null
        };
    }

    private HttpClient CreateClient()
    {
        var client = new HttpClient();
        client.DefaultRequestHeaders.Add("Authorization", $"Bearer {config.AccessToken}");
        return client;
    }
}
