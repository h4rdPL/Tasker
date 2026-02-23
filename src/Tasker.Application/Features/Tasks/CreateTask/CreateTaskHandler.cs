using Tasker.Domain.Entities;
using Tasker.Domain.Interfaces;
namespace Tasker.Application.Features.Tasks.CreateTask;
public class CreateTaskHandler
{
    private readonly ITaskRepository _taskRepository;
    private readonly IUserRepository _userRepository;
    private readonly IProjectRepository _projectRepository;

    public CreateTaskHandler(ITaskRepository taskRepository, IUserRepository userRepository, IProjectRepository projectRepository)
    {
        _taskRepository = taskRepository;
        _userRepository = userRepository;
        _projectRepository = projectRepository;
    }

    public async Task<Guid> Handle(CreateTaskCommand command, Guid userId, CancellationToken ct)
    {
        var task = new TaskItem(
            command.Title,
            command.Description,
            command.Deadline,
            command.Priority
        );

        if (command.ProjectId.HasValue)
        {
            var project = await _projectRepository.GetByIdAsync(command.ProjectId.Value, ct);
            if (project == null)
                throw new Exception("Project not found");

            task.ProjectId = project.Id;
        }

        if (command.Tags != null)
        {
            foreach (var tagName in command.Tags)
            {
                var tag = new Tag(tagName);
                task.AddTag(tag);
            }
        }

        var user = await _userRepository.GetByIdAsync(userId, ct);
        if (user == null)
            throw new Exception("User not found");

        task.UserId = user.Id;
        user.Tasks.Add(task);

        await _taskRepository.AddAsync(task, ct);

        return task.Id;
    }
}

