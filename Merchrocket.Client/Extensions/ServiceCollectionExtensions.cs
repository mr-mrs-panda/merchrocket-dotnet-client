using Merchrocket.Client.Endpoints;
using Merchrocket.Client.Models.Config;
using Microsoft.Extensions.DependencyInjection;

namespace Merchrocket.Client.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMerchrocketClient(this IServiceCollection serviceCollection,
        MerchrocketConfig config)
    {
        return serviceCollection
            .AddSingleton(config)
            .AddScoped<IHydraClient, HydraClient>()
            .AddScoped<IMerchrocketClient, MerchrocketClient>()
            .AddScoped<ICatalogProductEndpoint, CatalogProductEndpoint>()
            .AddScoped<ICatalogProductVariantEndpoint, CatalogProductVariantEndpoint>()
            .AddScoped<IMockupTaskEndpoint, MockupTaskEndpoint>();
    }
}