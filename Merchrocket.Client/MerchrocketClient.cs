using Merchrocket.Client.Endpoints;

namespace Merchrocket.Client;

public interface IMerchrocketClient
{
    ICatalogProductEndpoint CatalogProduct { get; }
    ICatalogProductVariantEndpoint CatalogProductVariant { get; }
    IMockupTaskEndpoint MockupTask { get; }
    IOrderEndpoint Order { get; }
    IStatisticsEndpoint Statistics { get; }
    IUserEndpoint User { get; }
}

public class MerchrocketClient(
    ICatalogProductEndpoint catalogProductEndpoint, 
    ICatalogProductVariantEndpoint catalogProductVariantEndpoint,
    IMockupTaskEndpoint mockupTaskEndpoint,
    IOrderEndpoint orderEndpoint,
    IStatisticsEndpoint statisticsEndpoint,
    IUserEndpoint userEndpoint) : IMerchrocketClient
{
    public ICatalogProductEndpoint CatalogProduct => catalogProductEndpoint;
    public ICatalogProductVariantEndpoint CatalogProductVariant => catalogProductVariantEndpoint;
    public IMockupTaskEndpoint MockupTask => mockupTaskEndpoint;
    public IOrderEndpoint Order => orderEndpoint;
    public IStatisticsEndpoint Statistics => statisticsEndpoint;
    public IUserEndpoint User => userEndpoint;
}
