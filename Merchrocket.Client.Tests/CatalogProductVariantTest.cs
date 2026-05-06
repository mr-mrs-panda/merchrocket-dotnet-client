using Merchrocket.Client.Constants;

namespace Merchrocket.Client.Tests;

public class CatalogProductVariantTest : ATestBase
{
    [Fact]
    public async Task ShouldGetCatalogProductVariants()
    {
        var res = await Client.CatalogProductVariant.GetCollectionAsync(
            "952b416d-499b-4399-87d2-6a84a4c898c0", page: 1, itemsPerPage: 30, withAllAttributes: true);

        var collection = AssertSuccess(res);
        Assert.NotEmpty(collection.Members!);

        var firstVariant = collection.Members!.First();
        Assert.NotNull(firstVariant);

        var supplierSku = firstVariant.TryGetAttributeValue<int?>(AttributeCodes.SupplierSku);
        Assert.NotNull(supplierSku);
    }

    [Fact]
    public async Task ShouldGetCatalogProductVariantById()
    {
        // Get a variant from the collection first
        var res = await Client.CatalogProductVariant.GetCollectionAsync(
            "952b416d-499b-4399-87d2-6a84a4c898c0", page: 1, itemsPerPage: 1);
        var collection = AssertSuccess(res);
        Assert.NotEmpty(collection.Members!);

        var variantId = collection.Members![0].VariantId!;

        // Now fetch via flat path /api/catalog-product-variants/{id}
        var variant = AssertSuccess(await Client.CatalogProductVariant.GetAsync(variantId));
        Assert.Equal(variantId, variant.VariantId);
        Assert.NotNull(variant.Sku);
    }
}
