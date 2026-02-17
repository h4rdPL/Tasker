using Microsoft.EntityFrameworkCore;
using Tasker.Domain.Entities;
using Tasker.Domain.Interfaces;
using Tasker.Infrastructure.Persistence;

namespace Tasker.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;


        public UserRepository(AppDbContext context)
        {
            _context = context;   
        }

        public async Task AddAsync(User user, CancellationToken ct)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync(ct);
        }

        public async Task<User> GetByEmailAsync(string email, CancellationToken ct)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email, ct);
        }

        public async Task<User> GetByIdAsync(Guid id, CancellationToken ct)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Id == id, ct);
        }
    }
}
