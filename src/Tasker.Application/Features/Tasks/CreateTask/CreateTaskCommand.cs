using Tasker.Domain.Enums;
namespace Tasker.Application.Features.Tasks.CreateTask;

public record CreateTaskCommand(
    string Title,
    string? Description,
    DateTime? Deadline,
    List<string>? Tags,
    TaskPriority Priority,
    Guid? ProjectId
);