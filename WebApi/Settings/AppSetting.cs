using Serilog.Configuration;

namespace WebApi.Settings;

public class AppSetting
{
    public ConnectionStrings ConnectionStrings { get; set; } = new();
    public SwaggerSetting SwaggerSetting { get; set; } = new();
    public CorsSettings CorsSettings { get; set; } = new();
    public Jwt Jwt { get; set; } = new();
    public Dictionary<string, string> FileProvider { get; set; } = new();
    public LoggerSettings LoggerSettings { get; set; } = new();
    public LocalizationSettings LocalizationSettings { get; set; } = new();
    public MailSettings MailSettings { get; set; } = new();
}

public class ConnectionStrings
{
    public string SqlConnection { get; set; } = string.Empty;
}

public class Jwt
{
    public string Key { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public string Issuer { get; set; } = string.Empty;
    public int ExpireInMinutes { get; set; }
}

public class CorsSettings
{
    public bool AllowAll { get; set; }
    public string Angular { get; set; } = string.Empty;
    public string Blazor { get; set; } = string.Empty;
    public string React { get; set; } = string.Empty;
}

public class SwaggerSetting
{
    public bool EnableSwagger { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Version { get; set; } = string.Empty;
}

public class LoggerSettings
{
    public string AppName { get; set; } = "FSH.WebAPI";
    public string ElasticSearchUrl { get; set; } = string.Empty;
    public bool WriteToFile { get; set; } = false;
    public bool StructuredConsoleLogging { get; set; } = false;
    public string MinimumLogLevel { get; set; } = "Information";
}

public class LocalizationSettings
{
    public bool? EnableLocalization { get; set; }
    public string? ResourcesPath { get; set; }
    public string[]? SupportedCultures { get; set; }
    public string? DefaultRequestCulture { get; set; }
    public bool? FallbackToParent { get; set; }
}

public class MailSettings
{
    public string? From { get; set; }

    public string? Host { get; set; }

    public int Port { get; set; }

    public string? UserName { get; set; }

    public string? Password { get; set; }

    public string? DisplayName { get; set; }
    public string? TemplateFolder { get; set; }
}
