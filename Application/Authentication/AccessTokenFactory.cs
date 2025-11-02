using Application.Authentication.Interfaces;
using Application.Utils.Interfaces;
using Application.Wrappers;
using Domain.Enums;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Authentication
{
    public class AccessTokenFactory : IAccessTokenFactory
    {
        private readonly IConfiguration _configuration;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly ILogger<AccessTokenFactory> _logger;

        public AccessTokenFactory(IConfiguration configuration, IDateTimeProvider dateTimeProvider, ILogger<AccessTokenFactory> logger)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _dateTimeProvider = dateTimeProvider ?? throw new ArgumentNullException(nameof(dateTimeProvider));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public Result<string> GenerateJwtToken(int accountId, string accountUserName, AccountRole role, string? accountStatus)
        {
            var roleString = role.ToString();

            if (string.IsNullOrWhiteSpace(roleString))
            {
                _logger.LogWarning("Attempted to generate JWT token with null or empty role.");
                return Result<string>.Fail("Role cannot be null or empty.");
            }

            var secret = _configuration["Authentication:Jwt:Secret"];
            var issuer = _configuration["Authentication:Jwt:Issuer"];
            var audience = _configuration["Authentication:Jwt:Audience"];

            if (string.IsNullOrEmpty(secret) || string.IsNullOrEmpty(issuer) || string.IsNullOrEmpty(audience))
            {
                _logger.LogError("JWT configuration is incomplete.");
                return Result<string>.Fail("JWT configuration is missing or invalid.");
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, accountId.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, accountId.ToString()),
                new Claim(ClaimTypes.Name, accountUserName),
                new Claim("AccountStatus", accountStatus ?? "Unknown"),
                new Claim(ClaimTypes.Role, roleString)
            };

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: _dateTimeProvider.UtcNow.AddMinutes(30), 
                signingCredentials: creds
            );

            _logger.LogInformation("JWT token generated successfully for AccountId: {AccountId}", accountId);

            return Result<string>.Success(new JwtSecurityTokenHandler().WriteToken(token));
        }

        public Result<string> GenerateAccessToken(int accountId, string accountUserName, AccountRole role, string? accountStatus)
            => GenerateJwtToken(accountId, accountUserName, role, accountStatus);
    }
}
