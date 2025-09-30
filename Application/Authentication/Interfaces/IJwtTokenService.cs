using Application.Wrappers;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authentication.Interfaces
{
    public interface IJwtTokenService
    {
        Result<string> GenerateJwtToken(int userId, string userName, AccountRole roles, string accountStatus);
    }
}
