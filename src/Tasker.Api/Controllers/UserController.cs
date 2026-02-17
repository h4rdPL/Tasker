using Microsoft.AspNetCore.Mvc;
using Tasker.Application.Features.Users;

namespace Tasker.Api.Controllers;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly RegisterUserHandler _registerHandler;

    public UserController(RegisterUserHandler registerHandler)
    {
        _registerHandler = registerHandler;
    }


    [HttpPost("register")]
    public async Task<IActionResult> Register(
        [FromBody] RegisterUserCommand command,
        CancellationToken ct)
    {
        try
        {
            var userId = await _registerHandler.Handle(command, ct);
            return CreatedAtAction(nameof(Register), new { id = userId }, new { id = userId });
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
}
