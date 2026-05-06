using Merchrocket.Client.Models;
using Merchrocket.Client.Models.Hydra;

namespace Merchrocket.Client.Endpoints;

public interface ICatalogProductEndpoint
{
    Task<ApiResponse<HydraCollection<CatalogProduct>>> GetCollectionAsync(int page = 1, int itemsPerPage = 30);
    Task<ApiResponse<CatalogProduct>> GetAsync(string id);
}

public class CatalogProductEndpoint(IHydraClient client) : ICatalogProductEndpoint
{
    public async Task<ApiResponse<HydraCollection<CatalogProduct>>> GetCollectionAsync(int page = 1, int itemsPerPage = 30)
    {
        return await client.GetCollectionAsync<CatalogProduct>("catalog-products", page, itemsPerPage);
    }

    public async Task<ApiResponse<CatalogProduct>> GetAsync(string id)
    {
        return await client.GetAsync<CatalogProduct>($"catalog-products/{id}");
    }
}
