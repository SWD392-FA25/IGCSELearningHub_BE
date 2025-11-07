using System.Threading;
using System.Threading.Tasks;

namespace Application.Utils.Interfaces
{
    public interface IEmailSender
    {
        Task SendAsync(string to, string subject, string htmlBody, CancellationToken cancellationToken = default);
    }
}
