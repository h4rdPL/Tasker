using Tasker.Domain.Entities;

namespace Tasker.Application.Tasks;

public record TaskDto(
    Guid Id,
    string Title,
    string Description,
    TaskPriority Priority,
    Tasker.Domain.Entities.TaskStatus Status,
    DateTime? Deadline
);
