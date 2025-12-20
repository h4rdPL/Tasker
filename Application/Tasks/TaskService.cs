using Tasker.Domain.Entities;
using System.Threading.Tasks;
namespace Tasker.Application.Tasks;

public class TaskService : ITaskService
{
    private static readonly List<TaskItem> _tasks = new();

    public Task<TaskDto> CreateAsync(
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

        return Task.FromResult(Map(task));
    }

    public Task<IEnumerable<TaskDto>> GetAllAsync(Guid userId)
    {
        var tasks = _tasks
            .Where(t => t.OwnerUserId == userId)
            .Select(Map);

        return Task.FromResult(tasks);
    }

    public Task<TaskDto?> GetByIdAsync(Guid id, Guid userId)
    {
        var task = _tasks.FirstOrDefault(
            t => t.Id == id && t.OwnerUserId == userId
        );

        return Task.FromResult(
            task is null ? null : Map(task)
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
