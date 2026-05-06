namespace Merchrocket.Client.Tests;

public class CatalogProductTest : ATestBase
{
    [Fact]
    public async Task ShouldGetCatalogProducts()
    {
        var res = await Client.CatalogProduct.GetCollectionAsync(1, 30);
        var collection = AssertSuccess(res);
        Assert.NotEmpty(collection.Members!);
    }
}
