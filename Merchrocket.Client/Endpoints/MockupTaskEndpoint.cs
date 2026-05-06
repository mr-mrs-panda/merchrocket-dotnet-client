using Merchrocket.Client.Models;

namespace Merchrocket.Client.Endpoints;

public interface IMockupTaskEndpoint
{
    /// <summary>
    /// Creates a new mockup task. Credit headers (X-MR-Credits-*) are
    /// available via <see cref="ApiResponse{T}.Credits"/>.
    /// </summary>
    Task<ApiResponse<MockupTask>> PostMockupTask(MockupTaskRequest request);

    /// <summary>
    /// Retrieves a mockup task by ID. Credit headers (X-MR-Credits-*) are
    /// available via <see cref="ApiResponse{T}.Credits"/>.
    /// </summary>
    Task<ApiResponse<MockupTask>> GetMockupTask(string id);
}

public class MockupTaskEndpoint(IHydraClient client) : IMockupTaskEndpoint
{
    public async Task<ApiResponse<MockupTask>> PostMockupTask(MockupTaskRequest request)
    {
        return await client.PostAsync<MockupTask, MockupTaskRequest>("mockup-tasks", request);
    }
    
    public async Task<ApiResponse<MockupTask>> GetMockupTask(string id)
    {
        return await client.GetAsync<MockupTask>($"mockup-tasks/{id}");
    }
}
