using WebApi.Common.Interfaces;

namespace WebApi.Interfaces.Common;

public interface IEmailTemplateService : ITransientService
{
    string GenerateEmailTemplate<T>(string templateName, T mailTemplateModel);
}
