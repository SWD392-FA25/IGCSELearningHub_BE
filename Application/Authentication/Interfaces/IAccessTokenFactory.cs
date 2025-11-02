using Application.Wrappers;
using Domain.Enums;

namespace Application.Authentication.Interfaces
{
    public interface IAccessTokenFactory
    {
        Result<string> GenerateAccessToken(int accountId, string accountUserName, AccountRole role, string? accountStatus);
    }
}

