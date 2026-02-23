using Microsoft.EntityFrameworkCore;
using Tasker.Domain.Entities;
using Tasker.Domain.Interfaces;
using Tasker.Infrastructure.Persistence;

namespace Tasker.Infrastructure.Repositories;

public class TaskCommentRepository : ITaskCommentRepository
{
    private readonly AppDbContext _context;

    public TaskCommentRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(TaskComment comment, CancellationToken ct)
    {
        await _context.TaskComments.AddAsync(comment, ct);
        await _context.SaveChangesAsync(ct);
    }

    public async Task<List<TaskComment>> GetByTaskIdAsync(Guid taskId, CancellationToken ct)
    {
        return await _context.TaskComments
            .Where(c => c.TaskId == taskId)
            .OrderBy(c => c.CreatedAt)
            .ToListAsync(ct);
    }
}