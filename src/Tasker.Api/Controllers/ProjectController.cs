using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Tasker.Application.Features.Projects.CreateProject;
using Tasker.Application.Features.Tasks.CreateProject;

namespace Tasker.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/projects")]
public class ProjectController : ControllerBase
{
    private readonly CreateProjectHandler _handler;

    public ProjectController(CreateProjectHandler handler)
    {
        _handler = handler;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateProjectCommand command, CancellationToken ct)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

        if (userIdClaim == null)
            return Unauthorized("User ID claim missing");

        var ownerId = Guid.Parse(userIdClaim.Value);

        var id = await _handler.Handle(command, ownerId, ct);

        return CreatedAtAction(nameof(Create), new { id }, id);
    }
}