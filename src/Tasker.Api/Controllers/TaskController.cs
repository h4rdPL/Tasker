using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Tasker.Application.Features.Tasks.CreateTask;

namespace Tasker.Api.Controllers;
[Authorize]
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
    [Authorize]
    public async Task<IActionResult> Create(
        [FromBody] CreateTaskCommand command,
        CancellationToken ct)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

        if (userIdClaim == null)
            return Unauthorized("User ID claim missing");

        var userId = Guid.Parse(userIdClaim.Value);

        var id = await _handler.Handle(command, userId, ct);

        return CreatedAtAction(nameof(Create), new { id }, id);
    }
}
