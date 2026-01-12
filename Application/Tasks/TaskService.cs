using Tasker.Domain.Common;
using Tasker.Domain.Entities;

namespace Tasker.Application.Tasks;

public class TaskService : ITaskService
{
    private static readonly List<TaskItem> _tasks = new();

    public Task<Result<TaskDto>> CreateAsync(
        CreateTaskRequest request,
        Guid userId)
    {
        var task = new TaskItem(
            request.Title,
            request.Description,
            request.Priority,
            request.Deadline,
            userId
        );

        _tasks.Add(task);

        return Task.FromResult(
            Result<TaskDto>.Success(Map(task))
        );
    }

    public Task<Result<IEnumerable<TaskDto>>> GetAllAsync(Guid userId)
    {
        var tasks = _tasks
            .Where(t => t.OwnerUserId == userId)
            .Select(Map)
            .ToList();

        return Task.FromResult(
            Result<IEnumerable<TaskDto>>.Success(tasks)
        );
    }

    public Task<Result<TaskDto>> GetByIdAsync(Guid id, Guid userId)
    {
        var task = _tasks.FirstOrDefault(
            t => t.Id == id && t.OwnerUserId == userId
        );

        if (task is null)
        {
            return Task.FromResult(
                Result<TaskDto>.Failure(TaskErrors.NotFound)
            );
        }

        return Task.FromResult(
            Result<TaskDto>.Success(Map(task))
        );
    }

    private static TaskDto Map(TaskItem task) =>
        new(
            task.Id,
            task.Title,
            task.Description,
            task.Priority,
            task.Status,
            task.Deadline
        );
}
