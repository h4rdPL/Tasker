using System.ComponentModel.DataAnnotations;

namespace Tasker.Application.Users;

public record RegisterUserRequest(
    [Required, EmailAddress] string Email,
    [Required, MinLength(3)] string Username,
    [Required, MinLength(6)] string Password
);
