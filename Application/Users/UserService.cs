using Tasker.Domain.Entities;
using Tasker.Infrastructure.Helpers;

namespace Tasker.Application.Users;

public class UserService : IUserService
{
    private static readonly List<User> _users = new();
    private readonly PasswordHasher _hasher; 

    public UserService(PasswordHasher hasher)   
    {
        _hasher = hasher;
    }

    public Task<UserDto> CreateAsync(CreateUserRequest request)
    {
        var hash = _hasher.Hash(request.Password);  
        var user = new User(request.Email, request.Username, hash);
        _users.Add(user);

        return Task.FromResult(Map(user));
    }

    public Task<UserDto?> GetByIdAsync(Guid id)
    {
        var user = _users.FirstOrDefault(x => x.Id == id);
        return Task.FromResult(user is null ? null : Map(user));
    }

    public Task<IEnumerable<UserDto>> GetAllAsync()
    {
        return Task.FromResult(_users.Select(Map).AsEnumerable());
    }

    private static UserDto Map(User user) =>
        new(
            user.Id,
            user.Email,
            user.Username,
            user.CreatedAt
        );
}
