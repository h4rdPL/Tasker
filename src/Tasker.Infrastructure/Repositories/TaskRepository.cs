using Microsoft.EntityFrameworkCore;
using Tasker.Domain.Entities;
using Tasker.Domain.Interfaces;

namespace Tasker.Infrastructure.Persistence.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly AppDbContext _context;

    public TaskRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(TaskItem task, CancellationToken ct)
    {
        await _context.Tasks.AddAsync(task, ct);
        await _context.SaveChangesAsync(ct);
    }

    public async Task<TaskItem?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        return await _context.Tasks
            .Include(t => t.Tags)
            .Include(t => t.Comments)
            .FirstOrDefaultAsync(t => t.Id == id, ct);
    }
    public async Task UpdateAsync(TaskItem task, CancellationToken ct)
    {
        await _context.SaveChangesAsync(ct);
    }
}
