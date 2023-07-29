using WebApi.Settings;

namespace WebApi.Cors;

public static class Startup
{

    private const string CorsPolicy = nameof(CorsPolicy);

    internal static IServiceCollection AddCorsPolicy(this IServiceCollection services)
    {
        var corsSettings = services.BuildServiceProvider().GetRequiredService<AppSetting>().CorsSettings;

        if (corsSettings == null) return services;

        if (Convert.ToBoolean(corsSettings.AllowAll))
        {
            //return services.AddCors(opt =>
            //    opt.AddPolicy(CorsPolicy, policy =>
            //        policy.AllowAnyHeader()
            //            .AllowAnyMethod()
            //            .AllowAnyOrigin()));

            return services.AddCors(opt =>
                opt.AddPolicy(CorsPolicy, policy =>
                    policy.AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials()
                        .SetIsOriginAllowed((hosts) => true)));

            //return builder.AllowAnyMethod().AllowAnyHeader().AllowCredentials().SetIsOriginAllowed((hosts) => true)
        }
        else
        {
            var origins = new List<string>();
            if (corsSettings.Angular is not null)
                origins.AddRange(corsSettings.Angular.Split(';', StringSplitOptions.RemoveEmptyEntries));
            if (corsSettings.Blazor is not null)
                origins.AddRange(corsSettings.Blazor.Split(';', StringSplitOptions.RemoveEmptyEntries));
            if (corsSettings.React is not null)
                origins.AddRange(corsSettings.React.Split(';', StringSplitOptions.RemoveEmptyEntries));

            return services.AddCors(opt =>
                opt.AddPolicy(CorsPolicy, policy =>
                    policy.AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials()
                        .WithOrigins(origins.ToArray())));
        }

    }

    internal static IApplicationBuilder UseCorsPolicy(this IApplicationBuilder app) =>
        app.UseCors(CorsPolicy);
}
