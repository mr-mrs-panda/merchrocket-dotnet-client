using Merchrocket.Client.Models;

namespace Merchrocket.Client.Tests;

public class UserTest : ATestBase
{
    [Fact]
    public async Task ShouldGetUsersAsync()
    {
        var response = await Client.User.GetCollectionAsync(page: 1, itemsPerPage: 5);
        var users = AssertSuccess(response);

        Assert.NotNull(users.Members);
        Assert.True(users.TotalItems >= 0);

        foreach (var user in users.Members)
        {
            Assert.NotNull(user.UserId);
            Assert.NotNull(user.Email);
        }
    }

    [Fact]
    public async Task ShouldGetUserByIdAsync()
    {
        var users = AssertSuccess(await Client.User.GetCollectionAsync(page: 1, itemsPerPage: 1));
        Assert.NotNull(users.Members);
        Assert.NotEmpty(users.Members);

        var userId = users.Members[0].UserId!;
        var user = AssertSuccess(await Client.User.GetAsync(userId));

        Assert.Equal(userId, user.UserId);
        Assert.NotNull(user.Email);
    }
}
