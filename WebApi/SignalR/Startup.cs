namespace WebApi.SignalR;

public static class Startup
{
    public static IServiceCollection AddSignals(this IServiceCollection services)
    {
        services.AddSignalR();
        return services;
    }

    public static IApplicationBuilder UseSignals(this WebApplication app)
    {
        app.MapHub<MessageHub>("/notifications");
        return app;
    }
}
