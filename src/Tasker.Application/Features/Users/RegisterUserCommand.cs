namespace Tasker.Application.Features.Users;

public sealed record RegisterUserCommand(
    string Email,
    string Password
);
