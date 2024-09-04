using Merchrocket.Client.Models;
using Merchrocket.Client.Models.Hydra;

namespace Merchrocket.Client.Endpoints;

public interface ICatalogProductVariantEndpoint
{
    Task<HydraCollection<CatalogProductVariant>> GetCollectionAsync(
        string catalogProductId,
        int page,
        int maxItemsPerPage,
        bool withAllAttributes = false);

    Task<CatalogProductVariant> GetAsync(string catalogProductId, string id);
}

public class CatalogProductVariantEndpoint(IHydraClient client) : ICatalogProductVariantEndpoint
{
    public async Task<HydraCollection<CatalogProductVariant>> GetCollectionAsync(
        string catalogProductId, 
        int page,
        int maxItemsPerPage,
        bool withAllAttributes = false)
    {
        var parameters = new Dictionary<string, string>
        {
            { "withAllAttributes", withAllAttributes.ToString().ToLower() } 
        };
        
        return await client.GetCollectionAsync<CatalogProductVariant>(
            $"/catalog-products/{catalogProductId}/catalog-variants", page, maxItemsPerPage, parameters);
    }
    
    public async Task<CatalogProductVariant> GetAsync(string catalogProductId, string id)
    {
        return await client.GetAsync<CatalogProductVariant>($"/catalog-products/{catalogProductId}/catalog-variants/{id}");
    }
}