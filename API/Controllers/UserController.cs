using Microsoft.AspNetCore.Mvc;
using Tasker.Application.Users;

namespace Tasker.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    public async Task<ActionResult<UserDto>> Create(CreateUserRequest request)
    {
        var user = await _userService.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<UserDto>> GetById(Guid id)
    {
        var user = await _userService.GetByIdAsync(id);
        if (user is null)
            return NotFound();

        return Ok(user);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetAll()
    {
        var users = await _userService.GetAllAsync();
        return Ok(users);
    }
}
