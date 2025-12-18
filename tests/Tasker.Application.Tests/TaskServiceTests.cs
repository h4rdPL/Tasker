using Tasker.Application.Tasks;
using Tasker.Domain.Entities;
using Xunit;

namespace Tasker.Application.Tests.Tasks;

public class TaskServiceTests
{
    private readonly TaskService _service;
    private readonly Guid _userId;
    private readonly Guid _otherUserId;

    public TaskServiceTests()
    {
        _service = new TaskService();
        _userId = Guid.NewGuid();
        _otherUserId = Guid.NewGuid();
    }

    [Fact]
    public async Task CreateAsync_ShouldCreateTask()
    {
        // Arrange
        var request = new CreateTaskRequest(
            Title: "Test task",
            Description: "Test description",
            Priority: TaskPriority.High,
            Deadline: null
        );

        // Act
        var result = await _service.CreateAsync(request, _userId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(request.Title, result.Title);
        Assert.Equal(request.Priority, result.Priority);
        Assert.Equal(Tasker.Domain.Entities.TaskStatus.Todo, result.Status);
        Assert.NotEqual(Guid.Empty, result.Id);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnOnlyUserTasks()
    {
        // Arrange
        await _service.CreateAsync(
            new CreateTaskRequest("User task", "", TaskPriority.Low, null),
            _userId
        );

        await _service.CreateAsync(
            new CreateTaskRequest("Other user task", "", TaskPriority.Low, null),
            _otherUserId
        );

        // Act
        var result = await _service.GetAllAsync(_userId);

        // Assert
        Assert.Single(result);
        Assert.Equal("User task", result.First().Title);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnTask_WhenOwnedByUser()
    {
        // Arrange
        var created = await _service.CreateAsync(
            new CreateTaskRequest("Owned task", "", TaskPriority.Medium, null),
            _userId
        );

        // Act
        var result = await _service.GetByIdAsync(created.Id, _userId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(created.Id, result!.Id);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnNull_WhenTaskBelongsToOtherUser()
    {
        // Arrange
        var created = await _service.CreateAsync(
            new CreateTaskRequest("Secret task", "", TaskPriority.High, null),
            _otherUserId
        );

        // Act
        var result = await _service.GetByIdAsync(created.Id, _userId);

        // Assert
        Assert.Null(result);
    }
}
