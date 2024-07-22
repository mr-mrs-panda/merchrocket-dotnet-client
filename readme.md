# Merchrocket Client

The Merchrocket Client is a comprehensive library designed to facilitate interaction with the Merchrocket API, providing a simplified way to access various endpoints such as Catalog Products, Catalog Product Variants, and Mockup Tasks.

## Features

- **Easy Configuration**: Configure the SDK with minimal setup using the `MerchrocketConfig`.
- **Comprehensive Endpoints**: Access to Catalog Products, Catalog Product Variants, and Mockup Tasks through well-defined interfaces.
- **Extensible**: Built with flexibility in mind, allowing for easy extension and customization.

## Getting Started

To get started with the Merchrocket Client SDK, you need to configure it with your project's `IServiceCollection`. This setup involves providing a `MerchrocketConfig` instance and registering the necessary services and endpoints.

### Prerequisites

Ensure you have a .NET project set up and ready to integrate with the Merchrocket Client SDK.

### Installation

1. First, add the Merchrocket Client SDK to your project. (This section will be updated with package installation instructions once available.)

2. Configure the `IServiceCollection` in your startup class or wherever you configure services:

```cs
using Merchrocket.Client.Extensions;
using Merchrocket.Client.Models.Config;

public void ConfigureServices(IServiceCollection services)
{
    var merchrocketConfig = new MerchrocketConfig
    {
        // Configuration properties here
    };

    services.AddMerchrocketClient(merchrocketConfig);
}
```

This will register the `MerchrocketClient` along with its dependencies and the endpoints: `CatalogProductEndpoint`, `CatalogProductVariantEndpoint`, and `MockupTaskEndpoint`.

### Creating a Mockup Task

To create a mockup task, you can use the `IMerchrocketClient` interface. Here's an example of how to create a mockup task:

```cs
using Merchrocket.Client.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class MockupTaskService
{
    private readonly IMerchrocketClient _merchrocketClient;

    public MockupTaskService(IMerchrocketClient merchrocketClient)
    {
        _merchrocketClient = merchrocketClient;
    }

    public async Task CreateMockupTaskAsync()
    {
        var mockupTaskRequest = new MockupTaskRequest
        {
            Format = "jpeg",
            RequestedProducts = new List<RequestedProduct>
            {
                new RequestedProduct
                {
                    CatalogProductId = "e2c2e18c-9626-4463-acfa-d5d5610f6c3a",
                    Placements = new List<Placement>
                    {
                        new Placement
                        {
                            Layers = new List<Layer>
                            {
                                new Layer
                                {
                                    Url = "https://d3r2wt7l0tcdny.cloudfront.net/products/mug_heart/print_source.jpg",
                                    Type = "file"
                                }
                            },
                            Technique = "direct_print",
                            Type = "main"
                        }
                    }
                }
            }
        };

        await _merchrocketClient.MockupTask.CreateMockupTaskAsync(mockupTaskRequest);
    }
}
```