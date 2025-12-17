using Tasker.Application.Users;
using Xunit;

namespace Tasker.Application.Tests.Users;

public class UserServiceTests
{
    private readonly UserService _service;

    public UserServiceTests()
    {
        _service = new UserService(); 
    }

    [Fact]
    public async Task CreateAsync_ShouldCreateUser()
    {
        // Arrange
        var request = new CreateUserRequest(
            Email: "john@test.com",
            Username: "john"
        );

        // Act
        var result = await _service.CreateAsync(request);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(request.Email, result.Email);
        Assert.Equal(request.Username, result.Username);
        Assert.NotEqual(Guid.Empty, result.Id);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnUser_WhenExists()
    {
        // Arrange
        var created = await _service.CreateAsync(
            new CreateUserRequest("anna@test.com", "anna")
        );

        // Act
        var result = await _service.GetByIdAsync(created.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(created.Id, result!.Id);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnNull_WhenNotFound()
    {
        // Act
        var result = await _service.GetByIdAsync(Guid.NewGuid());

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllUsers()
    {
        // Arrange
        await _service.CreateAsync(
            new CreateUserRequest("a@test.com", "a")
        );
        await _service.CreateAsync(
            new CreateUserRequest("b@test.com", "b")
        );

        // Act
        var result = await _service.GetAllAsync();

        // Assert
        Assert.Equal(2, result.Count());
    }
}
