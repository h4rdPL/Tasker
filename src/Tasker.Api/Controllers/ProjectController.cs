using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Tasker.Application.Features.Tasks.CreateProject;
using Tasker.Domain.Enums;

namespace Tasker.Api.Controllers
{


    [Authorize]
    [ApiController]
    [Route("api/projects")]
    public class ProjectController : ControllerBase
    {
        private readonly CreateProjectHandler _createProjectHandler;
        private readonly ChangeProjectMemberRoleHandler _roleHandler;

        public ProjectController(
            CreateProjectHandler createProjectHandler,
            ChangeProjectMemberRoleHandler roleHandler)
        {
            _createProjectHandler = createProjectHandler;
            _roleHandler = roleHandler;
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] CreateProjectCommand command,
            CancellationToken ct)
        {
            if (command is null)
                return BadRequest("Request body is required.");

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                return Unauthorized("User ID claim missing.");

            var ownerId = Guid.Parse(userIdClaim.Value);

            var projectId = await _createProjectHandler
                .Handle(command, ownerId, ct);

            return CreatedAtAction(
                nameof(GetById),
                new { id = projectId },
                new { id = projectId });
        }

        [HttpGet("{id:guid}")]
        public IActionResult GetById(Guid id)
        {
            return Ok(new { id });
        }


        [HttpPost("{projectId:guid}/members/{memberId:guid}/role")]
        public async Task<IActionResult> ChangeRole(
            Guid projectId,
            Guid memberId,
            [FromBody] ChangeRoleRequest request,
            CancellationToken ct)
        {
            if (request is null)
                return BadRequest("Request body is required.");

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                return Unauthorized("User ID claim missing.");

            var currentUserId = Guid.Parse(userIdClaim.Value);

            var command = new ChangeProjectMemberRoleCommand(
                projectId,
                memberId,
                request.NewRole);

            try
            {
                await _roleHandler.Handle(command, currentUserId, ct);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }


    public record ChangeRoleRequest(Role NewRole);

}
