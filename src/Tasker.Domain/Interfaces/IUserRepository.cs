using Tasker.Domain.Entities;

namespace Tasker.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task AddAsync(User user, CancellationToken ct);
        Task<User> GetByEmailAsync(string email, CancellationToken ct);
        Task<User> GetByIdAsync(Guid id, CancellationToken ct);
    }
}
