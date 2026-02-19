using Microsoft.AspNetCore.Mvc;
using Tasker.Application.Features.Users;
namespace Tasker.Api.Controllers;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly RegisterUserHandler _registerHandler;
    private readonly LoginHandler _loginHandler;
    public UserController(RegisterUserHandler registerHandler, LoginHandler loginHandler)
    {
        _registerHandler = registerHandler;
        _loginHandler = loginHandler;
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

    [HttpPost("login")]
    public async Task<IActionResult> Login(
    [FromBody] LoginRequest request,
    CancellationToken ct)
    {
        try
        {
            var token = await _loginHandler.Handle(request.Email, request.Password, ct);
            return Ok(new { token });
        }
        catch (Exception ex)
        {
            return Unauthorized(new { error = ex.Message });
        }
    }


}
