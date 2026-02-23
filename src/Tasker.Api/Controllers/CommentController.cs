using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Tasker.Application.Features.TaskComments;

namespace Tasker.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/tasks/{taskId}/comments")]
public class TaskCommentsController : ControllerBase
{
    private readonly TaskCommentHandler _handler;

    public TaskCommentsController(TaskCommentHandler handler)
    {
        _handler = handler;
    }


    [HttpPost]
    public async Task<IActionResult> Add(
        Guid taskId,
        [FromBody] AddTaskCommentRequest request,
        CancellationToken ct)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

        if (userIdClaim == null)
            return Unauthorized("User ID claim missing");

        var userId = Guid.Parse(userIdClaim.Value);

        var command = new AddTaskCommentCommand(
            taskId,
            request.Content
        );

        var id = await _handler.AddCommentAsync(command, userId, ct);

        return CreatedAtAction(nameof(Add), new { id }, id);
    }



    [HttpGet]
    public async Task<IActionResult> Get(
        Guid taskId,
        CancellationToken ct)
    {
        var query = new GetTaskCommentsQuery(taskId);

        var result = await _handler.GetCommentsAsync(query, ct);

        return Ok(result);
    }
}


public record AddTaskCommentRequest(string Content);