using Tasker.Domain.Entities;

namespace Tasker.Application.Tasks;

public class TaskService : ITaskService
{
    private static readonly List<TaskItem> _tasks = new();

    public System.Threading.Tasks.Task<TaskDto> CreateAsync(
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

        return System.Threading.Tasks.Task.FromResult(Map(task));
    }

    public System.Threading.Tasks.Task<IEnumerable<TaskDto>> GetAllAsync(Guid userId)
    {
        var tasks = _tasks
            .Where(t => t.OwnerUserId == userId)
            .Select(Map);

        return System.Threading.Tasks.Task.FromResult(tasks);
    }

    public System.Threading.Tasks.Task<TaskDto?> GetByIdAsync(Guid id, Guid userId)
    {
        var task = _tasks.FirstOrDefault(
            t => t.Id == id && t.OwnerUserId == userId
        );

        return System.Threading.Tasks.Task.FromResult(
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
