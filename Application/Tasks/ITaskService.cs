using Tasker.Domain.Common;

namespace Tasker.Application.Tasks;

public interface ITaskService
{
    Task<Result<TaskDto>> CreateAsync(CreateTaskRequest request, Guid userId);
    Task<Result<IEnumerable<TaskDto>>> GetAllAsync(Guid userId);
    Task<Result<TaskDto>> GetByIdAsync(Guid id, Guid userId);
}
