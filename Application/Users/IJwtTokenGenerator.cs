namespace Tasker.Application.Users;

public interface IJwtTokenGenerator
{
    string Generate(Guid userId, string email);
}
