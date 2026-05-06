using Merchrocket.Client.Constants;
using Merchrocket.Client.Models;

namespace Merchrocket.Client.Tests;

public class MockupTaskTest : ATestBase
{
    [Fact]
    public async Task ShouldGetMockupTasksAsync()
    {
        var response = await Client.MockupTask.GetMockupTasksAsync(page: 1, itemsPerPage: 5);
        var collection = AssertSuccess(response);
        Assert.NotNull(collection.Members);
        Assert.True(collection.TotalItems >= 0);

        foreach (var task in collection.Members)
        {
            Assert.NotNull(task.MockupTaskId);
            Assert.NotNull(task.Status);
        }
    }

    [Fact]
    public async Task ShouldCreateAndPollMockupTaskAsync()
    {
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

        var response = await Client.MockupTask.PostMockupTask(request);

        if (!response.IsSuccess)
        {
            Assert.Fail($"POST mockup-tasks failed: {response.Error}");
        }

        var task = AssertSuccess(response);
        Assert.NotNull(task.MockupTaskId);

        var mockupTaskId = task.MockupTaskId!;

        // Poll until finished (max 2 min)
        using var cts = new CancellationTokenSource(TimeSpan.FromMinutes(2));
        while (task.Status == "pending" && !cts.IsCancellationRequested)
        {
            await Task.Delay(TimeSpan.FromSeconds(5), cts.Token);
            var poll = await Client.MockupTask.GetMockupTask(mockupTaskId);
            task = AssertSuccess(poll);
        }

        Assert.NotNull(task.CatalogVariantMockups);
        Assert.Contains(task.CatalogVariantMockups, x => x.Mockups?.Any(m => m.MockupUrl != null) == true);

        // Download mockup assets (zip)
        var dl = await Client.MockupTask.DownloadMockupTaskAsync(mockupTaskId);
        var bytes = AssertSuccess(dl);
        Assert.True(bytes.Length > 0);
    }
}
