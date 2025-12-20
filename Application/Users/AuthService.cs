using Tasker.Domain.Entities;
using Tasker.Infrastructure.Helpers;

namespace Tasker.Application.Users;

public class AuthService : IAuthService
{
    private static readonly List<User> _users = new();
    private readonly PasswordHasher _hasher;
    private readonly IJwtTokenGenerator _jwt;

    public AuthService(
        PasswordHasher hasher,
        IJwtTokenGenerator jwt)
    {
        _hasher = hasher;
        _jwt = jwt;
    }

    public Task RegisterAsync(RegisterUserRequest request)
    {
        if (_users.Any(u => u.Email == request.Email))
            throw new Exception("User already exists");

        var hash = _hasher.Hash(request.Password);

        var user = new User(
            request.Email,
            request.Username,
            hash
        );

        _users.Add(user);

        return Task.CompletedTask;
    }

    public Task<AuthResult> LoginAsync(LoginRequest request)
    {
        var user = _users.FirstOrDefault(u => u.Email == request.Email);

        if (user is null || !_hasher.Verify(user.PasswordHash, request.Password))
            throw new Exception("Invalid credentials");

        var token = _jwt.Generate(user.Id, user.Email);

        return Task.FromResult(new AuthResult(
            user.Id,
            user.Email,
            user.Username,
            token
        ));
    }
}
