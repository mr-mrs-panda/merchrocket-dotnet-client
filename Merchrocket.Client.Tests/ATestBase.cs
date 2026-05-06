using Merchrocket.Client.Extensions;
using Merchrocket.Client.Models;
using Merchrocket.Client.Models.Config;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Merchrocket.Client.Tests;

public abstract class ATestBase
{
    protected IMerchrocketClient Client { get; }
    
    protected ATestBase()
    {
        var accessToken = Environment.GetEnvironmentVariable("MR_ACCESS_TOKEN");
        if (string.IsNullOrEmpty(accessToken))
        {
            throw new Exception("MR_ACCESS_TOKEN environment variable is not set.");
        }

        var baseUrl = Environment.GetEnvironmentVariable("MR_BASE_URL")
                      ?? "https://staging.merchrocket.shop/api/";
        
        var services = new ServiceCollection();
        services.AddLogging();
        services.AddMerchrocketClient(new MerchrocketConfig
        {
            BaseUrl = baseUrl,
            AccessToken = accessToken,
            RequestLogging = true,
            ThrowOnError = false
        });
        
        var sp = services.BuildServiceProvider();
        Client = sp.GetRequiredService<IMerchrocketClient>();
    }

    /// <summary>
    /// Assert that the API response was successful and has data.
    /// Returns the data or fails the test with the API error details.
    /// </summary>
    protected static T AssertSuccess<T>(ApiResponse<T> response)
    {
        if (!response.IsSuccess)
        {
            Assert.Fail($"API Error: {response.Error}");
        }
        Assert.NotNull(response.Data);
        return response.Data;
    }
}
