namespace Tasker.Application.Features.Tasks.CreateTask;

public record CreateTaskCommand(
    string Title,
    string? Description,
    DateTime? Deadline
);