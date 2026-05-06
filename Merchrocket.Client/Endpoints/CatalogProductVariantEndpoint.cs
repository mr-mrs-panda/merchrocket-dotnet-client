using Merchrocket.Client.Models;
using Merchrocket.Client.Models.Hydra;

namespace Merchrocket.Client.Endpoints;

public interface ICatalogProductVariantEndpoint
{
    Task<ApiResponse<HydraCollection<CatalogProductVariant>>> GetCollectionAsync(
        string catalogProductId,
        int page = 1,
        int itemsPerPage = 30,
        bool withAllAttributes = false);

    Task<ApiResponse<CatalogProductVariant>> GetAsync(string id);
}

public class CatalogProductVariantEndpoint(IHydraClient client) : ICatalogProductVariantEndpoint
{
    public async Task<ApiResponse<HydraCollection<CatalogProductVariant>>> GetCollectionAsync(
        string catalogProductId, 
        int page = 1,
        int itemsPerPage = 30,
        bool withAllAttributes = false)
    {
        var parameters = new Dictionary<string, string>
        {
            { "withAllAttributes", withAllAttributes.ToString().ToLower() } 
        };
        
        return await client.GetCollectionAsync<CatalogProductVariant>(
            $"catalog-products/{catalogProductId}/catalog-variants", page, itemsPerPage, parameters);
    }
    
    public async Task<ApiResponse<CatalogProductVariant>> GetAsync(string id)
    {
        return await client.GetAsync<CatalogProductVariant>($"catalog-product-variants/{id}");
    }
}
