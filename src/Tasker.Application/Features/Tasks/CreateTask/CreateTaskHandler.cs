using System;
using System.Threading;
using System.Threading.Tasks;
using Tasker.Domain.Interfaces;
using Tasker.Domain.Entities;
namespace Tasker.Application.Features.Tasks.CreateTask;
public class CreateTaskHandler
{
    private readonly ITaskRepository _taskRepository;
    private readonly IUserRepository _userRepository;

    public CreateTaskHandler(ITaskRepository taskRepository, IUserRepository userRepository)
    {
        _taskRepository = taskRepository;
        _userRepository = userRepository;
    }

    public async Task<Guid> Handle(CreateTaskCommand command, CancellationToken ct)
    {
        var task = new TaskItem(
            command.Title,
            command.Description,
            command.Deadline,
            command.Priority
        );

        if (command.Tags != null)
        {
            foreach (var tagName in command.Tags)
            {
                var tag = new Tag(tagName);
                task.AddTag(tag);
            }
        }

        if (command.UserId.HasValue)
        {
            var user = await _userRepository.GetByIdAsync(command.UserId.Value, ct);
            if (user == null)
                throw new Exception("User not found");

            task.UserId = user.Id;      
            user.Tasks.Add(task);     
        }

        await _taskRepository.AddAsync(task, ct);

        return task.Id;
    }
}
