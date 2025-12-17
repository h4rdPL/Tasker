namespace Tasker.Application.Users;

public interface IUserService
{
    Task<UserDto> CreateAsync(CreateUserRequest request);
    Task<UserDto?> GetByIdAsync(Guid id);
    Task<IEnumerable<UserDto>> GetAllAsync();
}
