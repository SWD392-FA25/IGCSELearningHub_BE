using Application.Authentication.Interfaces;
using Application.IRepository;
using Domain.Entities;

namespace Application.Authentication
{
    public class RefreshTokenManager : IRefreshTokenManager
    {
        private readonly IUnitOfWork _uow;
        private const int RefreshTokenDays = 7;

        public RefreshTokenManager(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<RefreshToken> CreateAsync(int accountId, string? ipAddress)
        {
            var token = Convert.ToBase64String(System.Security.Cryptography.RandomNumberGenerator.GetBytes(64));
            var rt = new RefreshToken
            {
                AccountId = accountId,
                Token = token,
                ExpiresAt = DateTime.UtcNow.AddDays(RefreshTokenDays),
                CreatedByIp = ipAddress
            };
            await _uow.RefreshTokenRepository.AddAsync(rt);
            await _uow.SaveChangesAsync();
            return rt;
        }

        public async Task<(bool Ok, RefreshToken? Token, string? Error)> ValidateActiveAsync(string token)
        {
            var rt = await _uow.RefreshTokenRepository.FindOneAsync(x => x.Token == token);
            if (rt == null) return (false, null, "Invalid refresh token.");
            if (rt.IsDeleted || rt.RevokedAt != null || rt.ExpiresAt <= DateTime.UtcNow)
                return (false, null, "Refresh token is expired or revoked.");
            return (true, rt, null);
        }

        public async Task<(bool Ok, RefreshToken? NewToken, string? Error)> RotateAsync(string token, string? ipAddress)
        {
            var check = await ValidateActiveAsync(token);
            if (!check.Ok || check.Token == null) return (false, null, check.Error);
            var old = check.Token;
            old.RevokedAt = DateTime.UtcNow;
            old.RevokedByIp = ipAddress;
            var @new = await CreateAsync(old.AccountId, ipAddress);
            old.ReplacedByToken = @new.Token;
            _uow.RefreshTokenRepository.Update(old);
            await _uow.SaveChangesAsync();
            return (true, @new, null);
        }

        public async Task<bool> RevokeByTokenAsync(string token, string? reason, string? ipAddress)
        {
            var rt = await _uow.RefreshTokenRepository.FindOneAsync(x => x.Token == token);
            if (rt == null) return false;
            if (rt.RevokedAt != null) return true;
            rt.RevokedAt = DateTime.UtcNow;
            rt.RevokedByIp = ipAddress;
            _uow.RefreshTokenRepository.Update(rt);
            await _uow.SaveChangesAsync();
            return true;
        }

        public async Task<int> RevokeAllForAccountAsync(int accountId, string? reason, string? ipAddress)
        {
            var q = _uow.RefreshTokenRepository.GetAllQueryable()
                .Where(x => x.AccountId == accountId && x.RevokedAt == null && !x.IsDeleted);
            var list = q.ToList();
            foreach (var t in list)
            {
                t.RevokedAt = DateTime.UtcNow;
                t.RevokedByIp = ipAddress;
                _uow.RefreshTokenRepository.Update(t);
            }
            await _uow.SaveChangesAsync();
            return list.Count;
        }
    }
}

