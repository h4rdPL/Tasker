using Tasker.Domain.Entities;

namespace Tasker.Domain.Interfaces
{
    public interface ITaskCommentRepository
    {
        Task AddAsync(TaskComment comment, CancellationToken ct);
        Task<List<TaskComment>> GetByTaskIdAsync(Guid taskId, CancellationToken ct);
    }
}
