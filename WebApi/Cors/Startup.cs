using WebApi.Settings;

namespace WebApi.Cors;

public static class Startup
{

    internal static IServiceCollection AddCorsPolicy(this IServiceCollection services)
    {

        services.AddCors(options => {
            options.AddPolicy("CorsPolicy", builder => builder.AllowAnyMethod().AllowAnyHeader().AllowCredentials().SetIsOriginAllowed((hosts) => true));
        });

        return services;

    }

    internal static IApplicationBuilder UseCorsPolicy(this IApplicationBuilder app) =>
        app.UseCors("CorsPolicy");
}
