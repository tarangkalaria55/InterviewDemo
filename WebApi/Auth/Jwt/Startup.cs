using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using WebApi.Settings;

namespace WebApi.Auth.Jwt;

public static class Startup
{
    public static IServiceCollection AddJwtAuth(this IServiceCollection services)
    {
        var setting = services.BuildServiceProvider().GetRequiredService<AppSetting>();
        var jwt = setting.Jwt;

        services.AddSingleton<IConfigureOptions<JwtBearerOptions>, ConfigureJwtBearerOptions>();

        return services
            .AddAuthentication(authentication =>
            {
                authentication.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authentication.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, null!)
            .Services;
    }
}
