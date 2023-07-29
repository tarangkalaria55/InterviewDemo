using Microsoft.Extensions.FileProviders;
using WebApi.Settings;

namespace WebApi.StaticFiles;

public static class Startup
{
    public static IApplicationBuilder UseStaticFilesSetup(this IApplicationBuilder app)
    {

        var settings = app.ApplicationServices.GetRequiredService<AppSetting>();

        app.UseStaticFiles(); // For the wwwroot folder

        if (settings.FileProvider.Count > 0)
        {

            foreach (var fileProvider in settings.FileProvider)
            {
                var filepath = Path.Combine(Directory.GetCurrentDirectory(), fileProvider.Key);
                var urlPath = fileProvider.Value;

                if (!Directory.Exists(filepath)) Directory.CreateDirectory(filepath);

                if (!urlPath.Trim().StartsWith("/")) urlPath = "/" + urlPath;

                app.UseStaticFiles(new StaticFileOptions()
                {
                    FileProvider = new PhysicalFileProvider(filepath),
                    RequestPath = new PathString(urlPath)
                });
            }



        }

        return app;
    }
}
