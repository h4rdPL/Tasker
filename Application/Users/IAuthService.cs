namespace Tasker.Application.Users;

public interface IAuthService
{
    Task RegisterAsync(RegisterUserRequest request);
    Task<AuthResult> LoginAsync(LoginRequest request);
}
