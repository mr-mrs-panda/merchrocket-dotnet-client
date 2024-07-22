using Merchrocket.Client.Models;

namespace Merchrocket.Client.Endpoints;

public interface IMockupTaskEndpoint
{
    Task<MockupTask> PostMockupTask(MockupTaskRequest request);
    Task<MockupTask> GetMockupTask(string id);
}

public class MockupTaskEndpoint(IHydraClient client) : IMockupTaskEndpoint
{
    public async Task<MockupTask> PostMockupTask(MockupTaskRequest request)
    {
        return await client.PostAsync<MockupTask, MockupTaskRequest>("mockup-tasks", request);
    }
    
    public async Task<MockupTask> GetMockupTask(string id)
    {
        return await client.GetAsync<MockupTask>($"mockup-tasks/{id}");
    }
}