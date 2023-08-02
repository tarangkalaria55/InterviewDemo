namespace WebApi.Settings;

public static class Startup
{
    public static IServiceCollection AddSettings(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<AppSetting>(option => configuration.Get<AppSetting>()!);
        return services;
    }
}
