using Merchrocket.Client.Endpoints;

namespace Merchrocket.Client;

public interface IMerchrocketClient
{
    ICatalogProductEndpoint CatalogProduct { get; }
    ICatalogProductVariantEndpoint CatalogProductVariant { get; }
    IMockupTaskEndpoint MockupTask { get; }
}

public class MerchrocketClient(
    ICatalogProductEndpoint catalogProductEndpoint, 
    ICatalogProductVariantEndpoint catalogProductVariantEndpoint,
    IMockupTaskEndpoint mockupTaskEndpoint) : IMerchrocketClient
{
    public ICatalogProductEndpoint CatalogProduct => catalogProductEndpoint;
    public ICatalogProductVariantEndpoint CatalogProductVariant => catalogProductVariantEndpoint;
    public IMockupTaskEndpoint MockupTask => mockupTaskEndpoint;
}