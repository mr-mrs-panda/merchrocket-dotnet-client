using Merchrocket.Client.Constants;
using Merchrocket.Client.Models;

namespace Merchrocket.Client.Tests;

public class MockupTaskTest : ATestBase
{
    [Fact]
    public async Task ShouldCreateMockupTaskAsync()
    {
        // Arrange
        var request = new MockupTaskRequest
        {
            Format = MockupTaskFormats.JPEG,
            RequestedProducts =
            [
                new RequestedProduct
                {
                    CatalogProductId = "e2c2e18c-9626-4463-acfa-d5d5610f6c3a",
                    Placements =
                    [
                        new Placement
                        {
                            Layers =
                            [
                                new Layer
                                {
                                    Url = "https://d3r2wt7l0tcdny.cloudfront.net/products/mug_heart/print_source.jpg",
                                    Type = PlacementLayerTypes.File
                                }
                            ],
                            Technique = PlacementTechniques.DirectPrint,
                            Type = PlacementTypes.Main
                        }
                    ]
                }
            ]
        };

        // Act
        var res = await Client.MockupTask.PostMockupTask(request);

        // Assert
        Assert.NotNull(res);
        Assert.NotNull(res.MockupTaskId);

        var mockupTaskId = res.MockupTaskId;

        while (res.Status == "pending")
        {
            await Task.Delay(TimeSpan.FromSeconds(5));
            res = await Client.MockupTask.GetMockupTask(mockupTaskId);
        }

        Assert.NotNull(res.CatalogVariantMockups);
        Assert.Contains(res.CatalogVariantMockups, x => x.Mockups?.Any(m => m.MockupUrl != null) == true);
    }
}