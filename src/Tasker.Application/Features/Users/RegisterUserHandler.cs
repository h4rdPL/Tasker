using Tasker.Application.Interfaces;
using Tasker.Domain.Entities;
using Tasker.Domain.Interfaces;

namespace Tasker.Application.Features.Users
{
    public class RegisterUserHandler
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;

        public RegisterUserHandler(
            IUserRepository userRepository,
            IPasswordHasher passwordHasher
            )
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }
        public async Task<Guid> Handle(RegisterUserCommand command, CancellationToken ct)
        {
            var existingUser = await _userRepository
                .GetByEmailAsync(command.Email, ct);

            if (existingUser != null)
                throw new Exception("User with this email already exists.");

            var passwordHash = _passwordHasher.Hash(command.Password);

            var user = new User(command.Email, passwordHash);

            await _userRepository.AddAsync(user, ct);

            return user.Id;
        }

    }
}
