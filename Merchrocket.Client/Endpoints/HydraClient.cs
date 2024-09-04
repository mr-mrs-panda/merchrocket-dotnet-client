using System.Net.Http.Json;
using Merchrocket.Client.Exceptions;
using Merchrocket.Client.Models.Config;
using Merchrocket.Client.Models.Hydra;
using Microsoft.Extensions.Logging;

namespace Merchrocket.Client.Endpoints;

public interface IHydraClient
{
    Task<HydraCollection<T>> GetCollectionAsync<T>(
        string path,
        int page = 1,
        int itemsPerPage = 30,
        Dictionary<string, string>? queryParams = null)
        where T : HydraMember;

    Task<T> GetAsync<T>(string path) where T : HydraMember;

    Task<TResponse> PostAsync<TResponse, TRequest>(string path, TRequest request) where TResponse : HydraMember;
}

public class HydraClient(MerchrocketConfig config, ILogger<HydraClient> logger) : IHydraClient
{
    public async Task<HydraCollection<T>> GetCollectionAsync<T>(
        string path,
        int page = 1,
        int itemsPerPage = 30,
        Dictionary<string, string>? queryParams = null)
        where T : HydraMember
    {
        var internalQueryParams = new Dictionary<string, string>
        {
            { "page", page.ToString() }, 
            { "itemsPerPage", itemsPerPage.ToString() }
        };
        
        if (queryParams != null)
        {
            foreach (var (key, value) in queryParams)
            {
                internalQueryParams[key] = value;
            }
        }
        
        var queryString = string.Join("&", internalQueryParams.Select(kvp => $"{kvp.Key}={kvp.Value}"));

        return await SendRequestAsync<HydraCollection<T>>(HttpMethod.Get, $"{path}?{queryString}");
    }

    public async Task<T> GetAsync<T>(string path) where T : HydraMember
    {
        return await SendRequestAsync<T>(HttpMethod.Get, path);
    }

    public async Task<TResponse> PostAsync<TResponse, TRequest>(string path, TRequest request)
        where TResponse : HydraMember
    {
        return await SendRequestAsync<TResponse>(HttpMethod.Post, path, JsonContent.Create(request));
    }

    private async Task<T> SendRequestAsync<T>(HttpMethod method, string path, HttpContent? content = null)
    {
        using var client = GetClient();

        var url = Path.Combine(config.BaseUrl.TrimEnd('/'), path.TrimStart('/'));

        try
        {
            if (config.RequestLogging)
            {
                logger.LogInformation("Merchrocket.Client | {Method} {Path}", method, path);
            }

            var request = new HttpRequestMessage(method, url) { Content = content };
            var response = await client.SendAsync(request);
            await LogResponseAsync(response);

            response.EnsureSuccessStatusCode();

            var responseData = await response.Content.ReadFromJsonAsync<T>();
            return responseData ?? throw new MerchrocketApiException("Can't parse responseData");
        }
        catch (Exception ex)
        {
            throw new MerchrocketApiException($"Error while making {method} request to {url}", ex);
        }
    }

    private async Task LogResponseAsync(HttpResponseMessage response)
    {
        if (config.RequestLogging)
        {
            var responseContent = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                logger.LogInformation("Merchrocket.Client | Response {StatusCode}: {Response}",
                    response.StatusCode, responseContent);
            }
            else
            {
                logger.LogError("Merchrocket.Client | Error Response {StatusCode}: {Response}",
                    response.StatusCode, responseContent);
            }
        }
    }

    private HttpClient GetClient()
    {
        var client = new HttpClient();
        client.DefaultRequestHeaders.Add("Authorization", $"Bearer {config.AccessToken}");
        return client;
    }
}