using Merchrocket.Client.Models;

namespace Merchrocket.Client.Tests;

public class OrderTest : ATestBase
{
    [Fact]
    public async Task ShouldGetOrdersAsync()
    {
        var response = await Client.Order.GetOrdersAsync(page: 1, itemsPerPage: 5);
        var collection = AssertSuccess(response);
        Assert.NotNull(collection.Members);
        Assert.True(collection.TotalItems >= 0);

        foreach (var order in collection.Members)
        {
            Assert.NotNull(order.OrderId);
            Assert.NotNull(order.ExternalId);
            Assert.NotNull(order.Status);
        }
    }

    [Fact]
    public async Task ShouldGetOrderByIdAsync()
    {
        var list = AssertSuccess(await Client.Order.GetOrdersAsync(page: 1, itemsPerPage: 1));
        Assert.NotNull(list.Members);
        if (list.Members.Count == 0) return;

        var orderId = list.Members[0].OrderId!;
        var order = AssertSuccess(await Client.Order.GetOrderAsync(orderId));

        Assert.Equal(orderId, order.OrderId);
        Assert.NotNull(order.ExternalId);
        Assert.NotNull(order.Status);
        Assert.NotNull(order.Recipient);
    }

    [Fact]
    public async Task ShouldGetOrderByExternalIdAsync()
    {
        var list = AssertSuccess(await Client.Order.GetOrdersAsync(page: 1, itemsPerPage: 1));
        Assert.NotNull(list.Members);
        if (list.Members.Count == 0) return;

        var externalId = list.Members[0].ExternalId!;
        var order = AssertSuccess(await Client.Order.GetOrderAsync($"@{externalId}"));

        Assert.Equal(externalId, order.ExternalId);
    }

    [Fact]
    public async Task ShouldGetOrderShipmentsAsync()
    {
        var list = AssertSuccess(await Client.Order.GetOrdersAsync(page: 1, itemsPerPage: 10));
        Assert.NotNull(list.Members);
        if (list.Members.Count == 0) return;

        var orderId = list.Members[0].OrderId!;
        var shipments = AssertSuccess(await Client.Order.GetShipmentsAsync(orderId));
        Assert.NotNull(shipments);
    }

    [Fact]
    public async Task ShouldGetOrderItemsAsync()
    {
        var list = AssertSuccess(await Client.Order.GetOrdersAsync(page: 1, itemsPerPage: 10));
        Assert.NotNull(list.Members);

        var order = list.Members.FirstOrDefault(o => o.OrderItems?.Count > 0);
        if (order == null) return;

        var items = AssertSuccess(await Client.Order.GetOrderItemsAsync(order.OrderId!, page: 1, itemsPerPage: 5));
        Assert.NotNull(items.Members);
        Assert.True(items.TotalItems >= 0);
    }

    [Fact]
    public async Task ShouldGetSingleOrderItemAsync()
    {
        var list = AssertSuccess(await Client.Order.GetOrdersAsync(page: 1, itemsPerPage: 10));
        Assert.NotNull(list.Members);

        var order = list.Members.FirstOrDefault(o => o.OrderItems?.Count > 0);
        if (order == null) return;

        var itemId = order.OrderItems![0].ItemId!;
        var item = AssertSuccess(await Client.Order.GetOrderItemAsync(order.OrderId!, itemId));

        Assert.Equal(itemId, item.ItemId);
    }
}
