using Merchrocket.Client.Models;
using Merchrocket.Client.Models.Hydra;

namespace Merchrocket.Client.Endpoints;

public interface ICatalogProductEndpoint
{
    Task<HydraCollection<CatalogProduct>> GetCollectionAsync(int page, int maxItemsPerPage);
    Task<CatalogProduct> GetAsync(string id);
}

public class CatalogProductEndpoint(IHydraClient client) : ICatalogProductEndpoint
{
    public async Task<HydraCollection<CatalogProduct>> GetCollectionAsync(int page, int maxItemsPerPage)
    {
        return await client.GetCollectionAsync<CatalogProduct>("/catalog-products", page, maxItemsPerPage);
    }

    public async Task<CatalogProduct> GetAsync(string id)
    {
        return await client.GetAsync<CatalogProduct>($"/catalog-products/{id}");
    }
}