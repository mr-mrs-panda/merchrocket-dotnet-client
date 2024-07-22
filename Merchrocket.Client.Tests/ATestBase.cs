using Merchrocket.Client.Extensions;
using Merchrocket.Client.Models.Config;
using Microsoft.Extensions.DependencyInjection;

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
        
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddLogging();
        serviceCollection.AddMerchrocketClient(new MerchrocketConfig
        {
            BaseUrl = "https://staging.merchrocket.shop/api/",
            AccessToken = accessToken,
            RequestLogging = true
        });
        
        var serviceProvider = serviceCollection.BuildServiceProvider();
        Client = serviceProvider.GetRequiredService<IMerchrocketClient>();
    }
}