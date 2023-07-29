using WebApi.Auth.Jwt;

namespace WebApi.Auth;

public static class Startup
{
    internal static IServiceCollection AddAuth(this IServiceCollection services)
    {
        services
            .AddCurrentUser()
            .AddPermissions();

        services.AddJwtAuth();
        services.AddAuthorization();

        return services;
    }

    internal static IApplicationBuilder UseCurrentUser(this IApplicationBuilder app) =>
        app.UseMiddleware<CurrentUserMiddleware>();

    private static IServiceCollection AddCurrentUser(this IServiceCollection services) =>
        services
            .AddScoped<CurrentUserMiddleware>()
            .AddScoped<ICurrentUser, CurrentUser>()
            .AddScoped(sp => (ICurrentUserInitializer)sp.GetRequiredService<ICurrentUser>());

    private static IServiceCollection AddPermissions(this IServiceCollection services) => services;
    //services
    //    .AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>()
    //    .AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();


}
