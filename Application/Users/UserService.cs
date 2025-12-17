using Tasker.Domain.Entities;

namespace Tasker.Application.Users;

public class UserService : IUserService
{
    private static readonly List<User> _users = new();

    public Task<UserDto> CreateAsync(CreateUserRequest request)
    {
        var user = new User(request.Email, request.Username);
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
