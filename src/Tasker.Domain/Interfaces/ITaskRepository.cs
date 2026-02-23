using Tasker.Domain.Entities;

namespace Tasker.Domain.Interfaces;

public interface ITaskRepository
{
    Task AddAsync(TaskItem task, CancellationToken ct);
    Task<TaskItem?> GetByIdAsync(Guid id, CancellationToken ct);
    Task UpdateAsync(TaskItem task, CancellationToken ct);
}