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
        var response = await Client.MockupTask.PostMockupTask(request);

        // Assert
        Assert.NotNull(response);
        Assert.NotNull(response.Data);
        Assert.NotNull(response.Data.MockupTaskId);

        var mockupTaskId = response.Data.MockupTaskId;

        while (response.Data.Status == "pending")
        {
            await Task.Delay(TimeSpan.FromSeconds(5));
            response = await Client.MockupTask.GetMockupTask(mockupTaskId);
        }

        Assert.NotNull(response.Data.CatalogVariantMockups);
        Assert.Contains(response.Data.CatalogVariantMockups, x => x.Mockups?.Any(m => m.MockupUrl != null) == true);

        // Verify credit information is present (mockup tasks always return credit headers)
        Assert.NotNull(response.Credits);
        Assert.NotNull(response.Credits.CreditsIncluded);
        Assert.NotNull(response.Credits.CreditsUsed);
        Assert.True(response.Credits.CreditsIncluded > 0);
        Assert.True(response.Credits.CreditsUsed > 0);
    }
}
