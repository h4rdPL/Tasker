using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasker.Application.Interfaces;
using Tasker.Domain.Interfaces;

namespace Tasker.Application.Features.Users
{
    public class LoginHandler
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IPasswordHasher _passwordHasher;

        public LoginHandler(
            IUserRepository userRepository,
            IJwtTokenGenerator jwtTokenGenerator,
            IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
            _passwordHasher = passwordHasher;
        }

        public async Task<string> Handle(string email, string password, CancellationToken ct)
        {
            var user = await _userRepository.GetByEmailAsync(email, ct);

            if (user == null)
                throw new Exception("Invalid credentials");

            if (!_passwordHasher.Verify(password, user.PasswordHash))
                throw new Exception("Invalid credentials");

            return _jwtTokenGenerator.GenerateToken(user);
        }
    }

}
