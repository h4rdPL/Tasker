using System.ComponentModel.DataAnnotations;

namespace Tasker.Application.Users;
    public record LoginRequest(
        [Required, EmailAddress] string Email,
        [Required, MinLength(6)] string Password
    );
