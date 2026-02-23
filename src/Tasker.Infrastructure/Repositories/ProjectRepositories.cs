using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Tasker.Domain.Entities;
using Tasker.Domain.Interfaces;
using Tasker.Infrastructure.Persistence;

namespace Tasker.Infrastructure.Repositories
{

public class ProjectRepository : IProjectRepository
{
    private readonly AppDbContext _dbContext;

    public ProjectRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(Project project, CancellationToken ct)
    {
        await _dbContext.Projects.AddAsync(project, ct);
        await _dbContext.SaveChangesAsync(ct);
    }

    public async Task<Project?> GetByIdAsync(Guid projectId, CancellationToken ct)
    {
        return await _dbContext.Projects
            .Include(p => p.Members) 
            .FirstOrDefaultAsync(p => p.Id == projectId, ct);
    }

    public async Task SaveChangesAsync(CancellationToken ct)
    {
        await _dbContext.SaveChangesAsync(ct);
    }
}
}
