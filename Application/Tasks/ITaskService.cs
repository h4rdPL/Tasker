namespace Tasker.Application.Tasks;

public interface ITaskService
{
    Task<TaskDto> CreateAsync(CreateTaskRequest request, Guid userId);
    Task<IEnumerable<TaskDto>> GetAllAsync(Guid userId);
    Task<TaskDto?> GetByIdAsync(Guid id, Guid userId);
}
