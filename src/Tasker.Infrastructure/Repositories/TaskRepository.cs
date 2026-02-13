using Microsoft.EntityFrameworkCore;
using Tasker.Domain.Entities;
using Tasker.Domain.Interfaces;
using Tasker.Infrastructure.Persistence;

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
}
