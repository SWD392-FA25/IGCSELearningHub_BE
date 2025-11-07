using Application.Authentication.Interfaces;
using FirebaseAdmin.Auth;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Authentication
{
    public class FirebaseExternalAuthProvider : IExternalAuthProvider
    {
        private readonly FirebaseAuth _firebaseAuth;
        private readonly ILogger<FirebaseExternalAuthProvider> _logger;

        public FirebaseExternalAuthProvider(FirebaseAuth firebaseAuth, ILogger<FirebaseExternalAuthProvider> logger)
        {
            _firebaseAuth = firebaseAuth;
            _logger = logger;
        }

        public string ProviderName => "Google";

        public async Task<ExternalAuthUser> ValidateTokenAsync(string idToken, CancellationToken cancellationToken = default)
        {
            try
            {
                var decoded = await _firebaseAuth.VerifyIdTokenAsync(idToken, cancellationToken);
                var email = decoded.Claims.TryGetValue("email", out var emailObj) ? emailObj?.ToString() ?? string.Empty : string.Empty;
                var verified = decoded.Claims.TryGetValue("email_verified", out var verifiedObj) && Convert.ToBoolean(verifiedObj);
                var fullName = decoded.Claims.TryGetValue("name", out var nameObj) ? nameObj?.ToString() : null;
                var preferred = decoded.Claims.TryGetValue("preferred_username", out var preferredObj) ? preferredObj?.ToString() : null;
                return new ExternalAuthUser(email, verified, decoded.Uid, fullName, preferred);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Failed to validate Firebase token.");
                throw;
            }
        }
    }
}
