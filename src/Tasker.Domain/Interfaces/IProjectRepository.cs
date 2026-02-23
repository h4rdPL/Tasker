using Tasker.Domain.Entities;

namespace Tasker.Domain.Interfaces
{
    public interface IProjectRepository
    {
        Task AddAsync(Project project, CancellationToken ct);
        Task<Project?> GetByIdAsync(Guid projectId, CancellationToken ct);
    }
}
