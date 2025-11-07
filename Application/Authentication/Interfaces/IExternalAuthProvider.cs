using System.Threading;
using System.Threading.Tasks;

namespace Application.Authentication.Interfaces
{
    public interface IExternalAuthProvider
    {
        string ProviderName { get; }
        Task<ExternalAuthUser> ValidateTokenAsync(string idToken, CancellationToken cancellationToken = default);
    }

    public record ExternalAuthUser(
        string Email,
        bool EmailVerified,
        string ExternalId,
        string? FullName,
        string? PreferredUserName);
}
