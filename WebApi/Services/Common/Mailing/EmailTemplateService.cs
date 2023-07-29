using System.Text;
using WebApi.Interfaces.Common;
using RazorEngineCore;
using WebApi.Settings;

namespace WebApi.Services.Common.Mailing;

public class EmailTemplateService : IEmailTemplateService
{
    private MailSettings _settings;

    public EmailTemplateService(AppSetting setting)
    {
        _settings = setting.MailSettings;
    }

    public string GenerateEmailTemplate<T>(string templateName, T mailTemplateModel)
    {
        string template = GetTemplate(_settings.TemplateFolder ?? "", templateName);

        IRazorEngine razorEngine = new RazorEngine();
        IRazorEngineCompiledTemplate modifiedTemplate = razorEngine.Compile(template);

        return modifiedTemplate.Run(mailTemplateModel);
    }

    public static string GetTemplate(string templateFolder, string templateName)
    {
        string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        string tmplFolder = Path.Combine(baseDirectory, templateFolder);
        string filePath = Path.Combine(tmplFolder, $"{templateName}.cshtml");

        using var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        using var sr = new StreamReader(fs, Encoding.Default);
        string mailText = sr.ReadToEnd();
        sr.Close();

        return mailText;
    }
}
