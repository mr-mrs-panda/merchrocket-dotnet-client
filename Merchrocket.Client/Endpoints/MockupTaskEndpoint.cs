using Merchrocket.Client.Models;
using Merchrocket.Client.Models.Hydra;

namespace Merchrocket.Client.Endpoints;

public interface IMockupTaskEndpoint
{
    Task<ApiResponse<HydraCollection<MockupTask>>> GetMockupTasksAsync(int page = 1, int itemsPerPage = 30);
    Task<ApiResponse<MockupTask>> PostMockupTask(MockupTaskRequest request);
    Task<ApiResponse<MockupTask>> GetMockupTask(string id);
    Task<ApiResponse<byte[]>> DownloadMockupTaskAsync(string id);
}

public class MockupTaskEndpoint(IHydraClient client) : IMockupTaskEndpoint
{
    public async Task<ApiResponse<HydraCollection<MockupTask>>> GetMockupTasksAsync(int page = 1, int itemsPerPage = 30)
        => await client.GetCollectionAsync<MockupTask>("mockup-tasks", page, itemsPerPage);

    public async Task<ApiResponse<MockupTask>> PostMockupTask(MockupTaskRequest request)
        => await client.PostAsync<MockupTask, MockupTaskRequest>("mockup-tasks", request);

    public async Task<ApiResponse<MockupTask>> GetMockupTask(string id)
        => await client.GetAsync<MockupTask>($"mockup-tasks/{id}");

    public async Task<ApiResponse<byte[]>> DownloadMockupTaskAsync(string id)
        => await client.GetBytesAsync($"mockup-tasks/{id}/download");
}
