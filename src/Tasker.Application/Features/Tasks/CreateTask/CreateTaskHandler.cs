using Tasker.Domain.Entities;
using Tasker.Domain.Interfaces;

namespace Tasker.Application.Features.Tasks.CreateTask;

public class CreateTaskHandler
{
    private readonly ITaskRepository _repository;

    public CreateTaskHandler(ITaskRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(CreateTaskCommand command, CancellationToken ct)
    {
        var task = new TaskItem(
            command.Title,
            command.Description,
            command.Deadline
        );

        await _repository.AddAsync(task, ct);

        return task.Id;
    }
}
