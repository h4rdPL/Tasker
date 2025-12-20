using System.ComponentModel.DataAnnotations;

namespace Tasker.Application.Users;

public record CreateUserRequest(
    [Required, EmailAddress] string Email,
    [Required, MinLength(3)] string Username,
    string Password
);
