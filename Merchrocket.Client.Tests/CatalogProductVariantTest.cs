using Merchrocket.Client.Constants;

namespace Merchrocket.Client.Tests;

public class CatalogProductVariantTest : ATestBase
{
    [Fact]
    public async Task ShouldGetCatalogProductVariants()
    {
        // Arrange
        var eut = Client.CatalogProductVariant;

        // Act
        var res = await eut.GetCollectionAsync("952b416d-499b-4399-87d2-6a84a4c898c0", 1, 30, true);

        // Assert
        Assert.NotNull(res);
        Assert.NotNull(res.Data);
        Assert.NotEmpty(res.Data.Members!);
        
        var firstVariant = res.Data.Members!.First();
        Assert.NotNull(firstVariant);

        var supplierSku = firstVariant.TryGetAttributeValue<int?>(AttributeCodes.SupplierSku);
        Assert.NotNull(supplierSku);
    }
}
