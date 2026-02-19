using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasker.Domain.Entities;

namespace Tasker.Application.Features.Users
{
    public interface IJwtTokenGenerator
    {
            string GenerateToken(User user);
    }
}
