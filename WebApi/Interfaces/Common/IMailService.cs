using WebApi.Common.Interfaces;
using WebApi.Models.Common;

namespace WebApi.Interfaces.Common;

public interface IMailService : ITransientService
{
    Task SendAsync(MailRequest request, CancellationToken ct);
}
