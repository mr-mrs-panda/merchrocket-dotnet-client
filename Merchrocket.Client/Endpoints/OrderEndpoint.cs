using Merchrocket.Client.Models;
using Merchrocket.Client.Models.Hydra;

namespace Merchrocket.Client.Endpoints;

public interface IOrderEndpoint
{
    // Order operations
    Task<ApiResponse<HydraCollection<Order>>> GetOrdersAsync(int page = 1, int itemsPerPage = 30);
    Task<ApiResponse<Order>> GetOrderAsync(string id);
    Task<ApiResponse<Order>> CreateOrderAsync(CreateOrderRequest request);
    Task<ApiResponse<Order>> PatchOrderAsync(string id, PatchOrderRequest request);
    Task<ApiResponse<Order>> ConfirmOrderAsync(string id);
    Task<ApiResponse<Order>> CancelOrderAsync(string id);
    Task<ApiResponse<Order>> RefundOrderAsync(string id);

    // Shipment sub-resource
    Task<ApiResponse<List<ShipmentInfo>>> GetShipmentsAsync(string orderId);

    // OrderItem sub-resource (order-items = plural collection, order-item = singular by id)
    Task<ApiResponse<HydraCollection<OrderItem>>> GetOrderItemsAsync(string orderId, int page = 1, int itemsPerPage = 30);
    Task<ApiResponse<OrderItem>> GetOrderItemAsync(string orderId, string itemId);
    Task<ApiResponse<OrderItem>> CreateOrderItemAsync(string orderId, CreateOrderItemRequest request);
    Task<ApiResponse<OrderItem>> PatchOrderItemAsync(string orderId, string itemId, CreateOrderItemRequest request);
    Task<ApiResponse<object>> DeleteOrderItemAsync(string orderId, string itemId);
}

public class OrderEndpoint(IHydraClient client) : IOrderEndpoint
{
    public async Task<ApiResponse<HydraCollection<Order>>> GetOrdersAsync(int page = 1, int itemsPerPage = 30)
        => await client.GetCollectionAsync<Order>("orders", page, itemsPerPage);

    public async Task<ApiResponse<Order>> GetOrderAsync(string id)
        => await client.GetAsync<Order>($"orders/{id}");

    public async Task<ApiResponse<Order>> CreateOrderAsync(CreateOrderRequest request)
        => await client.PostAsync<Order, CreateOrderRequest>("orders", request);

    public async Task<ApiResponse<Order>> PatchOrderAsync(string id, PatchOrderRequest request)
        => await client.PatchAsync<Order, PatchOrderRequest>($"orders/{id}", request);

    public async Task<ApiResponse<Order>> ConfirmOrderAsync(string id)
        => await client.PostActionAsync<Order>($"orders/{id}/confirm");

    public async Task<ApiResponse<Order>> CancelOrderAsync(string id)
        => await client.PostActionAsync<Order>($"orders/{id}/cancel");

    public async Task<ApiResponse<Order>> RefundOrderAsync(string id)
        => await client.PostActionAsync<Order>($"orders/{id}/refund");

    public async Task<ApiResponse<List<ShipmentInfo>>> GetShipmentsAsync(string orderId)
        => await client.GetListAsync<ShipmentInfo>($"orders/{orderId}/shipments");

    // OrderItem sub-resources: /api/orders/{orderId}/order-items (plural) and /api/orders/{orderId}/order-item/{id} (singular)
    public async Task<ApiResponse<HydraCollection<OrderItem>>> GetOrderItemsAsync(string orderId, int page = 1, int itemsPerPage = 30)
        => await client.GetCollectionAsync<OrderItem>($"orders/{orderId}/order-items", page, itemsPerPage);

    public async Task<ApiResponse<OrderItem>> GetOrderItemAsync(string orderId, string itemId)
        => await client.GetAsync<OrderItem>($"orders/{orderId}/order-item/{itemId}");

    public async Task<ApiResponse<OrderItem>> CreateOrderItemAsync(string orderId, CreateOrderItemRequest request)
        => await client.PostAsync<OrderItem, CreateOrderItemRequest>($"orders/{orderId}/order-items", request);

    public async Task<ApiResponse<OrderItem>> PatchOrderItemAsync(string orderId, string itemId, CreateOrderItemRequest request)
        => await client.PatchAsync<OrderItem, CreateOrderItemRequest>($"orders/{orderId}/order-item/{itemId}", request);

    public async Task<ApiResponse<object>> DeleteOrderItemAsync(string orderId, string itemId)
        => await client.DeleteAsync($"orders/{orderId}/order-item/{itemId}");
}
