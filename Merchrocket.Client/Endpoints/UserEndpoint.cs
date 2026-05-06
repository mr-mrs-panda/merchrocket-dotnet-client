using Merchrocket.Client.Models;
using Merchrocket.Client.Models.Hydra;

namespace Merchrocket.Client.Endpoints;

public interface IUserEndpoint
{
    Task<ApiResponse<HydraCollection<User>>> GetCollectionAsync(int page = 1, int itemsPerPage = 30);
    Task<ApiResponse<User>> GetAsync(string id);
    Task<ApiResponse<object>> DeleteAsync(string id);
}

public class UserEndpoint(IHydraClient client) : IUserEndpoint
{
    public async Task<ApiResponse<HydraCollection<User>>> GetCollectionAsync(int page = 1, int itemsPerPage = 30)
        => await client.GetCollectionAsync<User>("users", page, itemsPerPage);

    public async Task<ApiResponse<User>> GetAsync(string id)
        => await client.GetAsync<User>($"users/{id}");

    public async Task<ApiResponse<object>> DeleteAsync(string id)
        => await client.DeleteAsync($"users/{id}");
}
