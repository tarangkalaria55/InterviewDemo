namespace WebApi.Settings;

public static class Startup
{
    public static IServiceCollection AddSettings(this IServiceCollection services, IConfiguration configuration)
    {
        var appSetting = new AppSetting();
        configuration.Bind(appSetting);
        services.AddSingleton(appSetting);
        return services;
    }
}
