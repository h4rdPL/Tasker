namespace Tasker.Application.Features.Tasks.CreateProject
{
    public record CreateProjectCommand(
        string Name,
        string? Description
    );
}
