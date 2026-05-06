# Merchrocket .NET Client

A comprehensive, strongly-typed .NET client library for the [Merchrocket](https://merchrocket.shop) print-on-demand API.

[![.NET](https://img.shields.io/badge/.NET-8.0%20%7C%209.0%20%7C%2010.0-512BD4)](https://dotnet.microsoft.com)
[![License](https://img.shields.io/badge/license-MIT-green)](LICENSE)

---

## Features

- **Typed API** — Catalog Products, Variants, Mockup Tasks, Orders, Order Items, Shipments, Statistics, Users
- **Error handling** — Defaults to throwing `MerchrocketApiException`; opt-in to `ApiResponse<T>` pattern via config
- **Credit tracking** — `X-MR-Credits-Included` / `X-MR-Credits-Used` headers extracted automatically
- **Pagination** — Hydra collections with `page`, `itemsPerPage`, `totalItems`, navigation links
- **DI-ready** — `AddMerchrocketClient()` extension registers all services
- **Multi-targeting** — `net8.0`, `net9.0`, `net10.0`
- **Logging** — Structured via `Microsoft.Extensions.Logging`; HTTP errors always logged at Warning

---

## Installation

```bash
dotnet add package Merchrocket.Client
```

---

## Getting Started

```csharp
using Merchrocket.Client.Extensions;
using Merchrocket.Client.Models.Config;

builder.Services.AddMerchrocketClient(new MerchrocketConfig
{
    BaseUrl = "https://app.merchrocket.shop/api/",
    AccessToken = "mrp_your_access_token",
    ThrowOnError = true,     // default: throws MerchrocketApiException on errors
    RequestLogging = false   // set true for per-request Info-level logging
});
```

Inject `IMerchrocketClient` into your services:

```csharp
public class MyService(IMerchrocketClient client)
{
    public async Task DoSomething()
    {
        var orders = await client.Order.GetOrdersAsync();
        // ...
    }
}
```

---

## Error Handling

**Default mode** (`ThrowOnError = true`) — throws exceptions:

```csharp
try
{
    var order = await client.Order.GetOrderAsync("bad-id");
}
catch (MerchrocketApiException ex)
{
    // ex.StatusCode, ex.ResponseBody, ex.RequestPath
}
```

**Opt-in mode** (`ThrowOnError = false`) — returns `ApiResponse<T>`:

```csharp
var response = await client.Order.GetOrderAsync("bad-id");
if (!response.IsSuccess)
{
    // response.Error.StatusCode, response.Error.ResponseBody
}
```

| `ApiResponse<T>` property | Description |
|---|---|
| `Data` | Deserialized body (valid when `IsSuccess`) |
| `IsSuccess` | `true` on 2xx |
| `Error` | `StatusCode`, `ResponseBody`, `RequestPath` on failure |
| `Credits` | `CreditsIncluded` / `CreditsUsed` from response headers |

---

## Endpoint Reference

| Property | Resources |
|---|---|
| `client.CatalogProduct` | Products in your catalog |
| `client.CatalogProductVariant` | Variants (SKUs, placements, attributes) |
| `client.MockupTask` | Mockup generation jobs |
| `client.Order` | Orders, order items, shipments, transitions |
| `client.Statistics` | Monthly mockup usage statistics |
| `client.User` | User accounts |

---

### 📦 Catalog Product

```csharp
var products = await client.CatalogProduct.GetCollectionAsync();
var product  = await client.CatalogProduct.GetAsync("e2c2e18c-...");
```

| Method | Endpoint |
|---|---|
| `GetCollectionAsync(page, itemsPerPage)` | `GET /api/catalog-products` |
| `GetAsync(id)` | `GET /api/catalog-products/{id}` |

### 🎨 Catalog Product Variant

```csharp
var variants = await client.CatalogProductVariant.GetCollectionAsync(
    "952b416d-...", withAllAttributes: true);
var variant  = await client.CatalogProductVariant.GetAsync("0587b207-...");

// Typed attribute access
var sku = variant.TryGetAttributeValue<int?>(AttributeCodes.SupplierSku);
```

| Method | Endpoint |
|---|---|
| `GetCollectionAsync(productId, ...)` | `GET /api/catalog-products/{id}/catalog-variants` |
| `GetAsync(id)` | `GET /api/catalog-product-variants/{id}` |

### 🖼️ Mockup Task

```csharp
var request = new MockupTaskRequest
{
    Format = MockupTaskFormats.JPEG,
    RequestedProducts =
    [
        new RequestedProduct
        {
            CatalogProductId = "e2c2e18c-...",
            Placements =
            [
                new Placement
                {
                    Type = PlacementTypes.Main,
                    Technique = PlacementTechniques.DirectPrint,
                    Layers = [new Layer { Url = "https://example.com/design.png", Type = PlacementLayerTypes.File }]
                }
            ]
        }
    ]
};

var task = await client.MockupTask.PostMockupTask(request);
var id = task.Data!.MockupTaskId!;

// Poll until done
while (task.Data.Status == "pending")
{
    await Task.Delay(5000);
    task = await client.MockupTask.GetMockupTask(id);
}

// Access results
foreach (var vm in task.Data.CatalogVariantMockups!)
foreach (var m in vm.Mockups!)
    Console.WriteLine(m.MockupUrl);

// Download all assets as ZIP
var zip = await client.MockupTask.DownloadMockupTaskAsync(id);

// List all tasks
var all = await client.MockupTask.GetMockupTasksAsync();
```

| Method | Endpoint |
|---|---|
| `GetMockupTasksAsync(page, itemsPerPage)` | `GET /api/mockup-tasks` |
| `PostMockupTask(request)` | `POST /api/mockup-tasks` |
| `GetMockupTask(id)` | `GET /api/mockup-tasks/{id}` |
| `DownloadMockupTaskAsync(id)` | `GET /api/mockup-tasks/{id}/download` |

### 📋 Order

```csharp
var order = await client.Order.CreateOrderAsync(new CreateOrderRequest
{
    ExternalId = "SHOP-12345",
    Shipping = "STANDARD",
    Recipient = new Recipient { Name = "Max Mustermann", Address1 = "Musterstr. 1", City = "Berlin", CountryCode = "DE", Zip = "10115" },
    OrderItems =
    [
        new CreateOrderItemRequest { ExternalId = "ITEM-001", Quantity = 1, VariantId = "0587b207-..." }
    ]
});

var orders   = await client.Order.GetOrdersAsync();
var byId     = await client.Order.GetOrderAsync(orderId);
var byExt    = await client.Order.GetOrderAsync("@SHOP-12345");

await client.Order.PatchOrderAsync(orderId, new PatchOrderRequest { Shipping = "DHL_EXPRESS" });
await client.Order.ConfirmOrderAsync(orderId);
// await client.Order.CancelOrderAsync(orderId);
// await client.Order.RefundOrderAsync(orderId);
```

| Method | Endpoint |
|---|---|
| `GetOrdersAsync(...)` | `GET /api/orders` |
| `GetOrderAsync(id)` | `GET /api/orders/{id}` |
| `CreateOrderAsync(req)` | `POST /api/orders` |
| `PatchOrderAsync(id, req)` | `PATCH /api/orders/{id}` |
| `ConfirmOrderAsync(id)` | `POST /api/orders/{id}/confirm` |
| `CancelOrderAsync(id)` | `POST /api/orders/{id}/cancel` |
| `RefundOrderAsync(id)` | `POST /api/orders/{id}/refund` |

### 📦 Order Items & 🚚 Shipments

```csharp
var items      = await client.Order.GetOrderItemsAsync(orderId);
var item       = await client.Order.GetOrderItemAsync(orderId, itemId);
var created    = await client.Order.CreateOrderItemAsync(orderId, new CreateOrderItemRequest { Quantity = 2, VariantId = "..." });
await client.Order.PatchOrderItemAsync(orderId, itemId, new CreateOrderItemRequest { Quantity = 3 });
await client.Order.DeleteOrderItemAsync(orderId, itemId);

var shipments  = await client.Order.GetShipmentsAsync(orderId);
```

| Method | Endpoint |
|---|---|
| `GetOrderItemsAsync(...)` | `GET /api/orders/{orderId}/order-items` |
| `GetOrderItemAsync(orderId, itemId)` | `GET /api/orders/{orderId}/order-item/{id}` |
| `CreateOrderItemAsync(orderId, req)` | `POST /api/orders/{orderId}/order-items` |
| `PatchOrderItemAsync(...)` | `PATCH /api/orders/{orderId}/order-item/{id}` |
| `DeleteOrderItemAsync(...)` | `DELETE /api/orders/{orderId}/order-item/{id}` |
| `GetShipmentsAsync(orderId)` | `GET /api/orders/{orderId}/shipments` |

### 📊 Statistics & 👤 Users

```csharp
var stats = await client.Statistics.GetMonthlyMockupStatsAsync("2026");
var users = await client.User.GetCollectionAsync();
var user  = await client.User.GetAsync(userId);
await client.User.DeleteAsync(userId);
```

---

## Constants

```csharp
MockupTaskFormats.JPEG                // "jpeg"
PlacementTypes.Main                   // "main"
PlacementTechniques.DirectPrint       // "direct_print"
PlacementTechniques.Engraving         // "engraving"
PlacementLayerTypes.File             // "file"
AttributeCodes.SupplierSku           // "supplierSku"
```

---

## Key Models

| Model | Key Properties |
|---|---|
| `CatalogProduct` | `ProductId`, `Name`, `Brand`, `Image`, `VariantCount` |
| `CatalogProductVariant` | `VariantId`, `Sku`, `Placements`, `Options`, `InStock`, `Attributes` |
| `MockupTaskRequest` / `MockupTask` | `Format`, `RequestedProducts` / `Status`, `CatalogVariantMockups`, `FailureReasons` |
| `CreateOrderRequest` / `Order` | `ExternalId`, `Shipping`, `Recipient`, `OrderItems` / `Status`, `Shipments`, `Errors` |
| `CreateOrderItemRequest` / `OrderItem` | `Quantity`, `VariantId`, `Placements` / `ItemId`, `Errors`, `Images` |
| `Recipient` | `Name`, `Address1`, `City`, `CountryCode`, `Zip`, `Email` |
| `ShipmentInfo` | `Carrier`, `TrackingId`, `TrackingUrl`, `ShipmentStatus` |
| `User` | `UserId`, `Email`, `Roles`, `IsOnline` |

---

## API Coverage

| Resource | GET collection | GET single | POST | PATCH | DELETE | Special |
|---|---|---|---|---|---|---|
| **CatalogProduct** | ✅ | ✅ | — | — | — | |
| **CatalogProductVariant** | ✅ | ✅ | — | — | — | |
| **MockupTask** | ✅ | ✅ | ✅ | — | — | Download |
| **Order** | ✅ | ✅ | ✅ | ✅ | — | confirm, cancel, refund |
| **OrderItem** | ✅ | ✅ | ✅ | ✅ | ✅ | |
| **Shipment** | ✅ | — | — | — | — | |
| **Statistics** | ✅ | — | — | — | — | |
| **User** | ✅ | ✅ | — | — | ✅ | |

---

## License

MIT — see [LICENSE](LICENSE).
