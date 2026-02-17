using Microsoft.AspNetCore.Mvc;
using Tasker.Application.Features.Tasks.CreateTask;

namespace Tasker.Api.Controllers;

[ApiController]
[Route("api/tasks")]
public class TasksController : ControllerBase
{
    private readonly CreateTaskHandler _handler;

    public TasksController(CreateTaskHandler handler)
    {
        _handler = handler;
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        CreateTaskCommand command,
        CancellationToken ct)
    {
        var id = await _handler.Handle(command, ct);

        return CreatedAtAction(nameof(Create), new { id }, id);
    }
}
