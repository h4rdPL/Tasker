namespace Tasker.Application.Users;

public record AuthResult(
    Guid UserId,
    string Email,
    string Username,
    string Token

);
