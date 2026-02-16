using Tasker.Domain.Entities;

namespace Tasker.Domain.Interfaces;

public interface ITaskRepository
{
    Task AddAsync(TaskItem task, CancellationToken ct);
}