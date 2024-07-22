namespace Merchrocket.Client.Tests;

public class CatalogProductTest: ATestBase
{
    [Fact]
    public async Task ShouldGetCatalogProducts()
    {
        // Arrange
        var eut = Client.CatalogProduct;

        // Act
        var res = await eut.GetCollectionAsync(1, 30);

        // Assert
        Assert.NotNull(res);
        Assert.NotEmpty(res.Members!);
    }
}