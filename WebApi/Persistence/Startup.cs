using Microsoft.EntityFrameworkCore;
using WebApi.Persistence.Context;
using WebApi.Settings;

namespace WebApi.Persistence;


public static class Startup
{
    public static IServiceCollection AddPersistence(this IServiceCollection services)
    {
        var setting = services.BuildServiceProvider().GetRequiredService<AppSetting>();

        services.AddDbContext<BookContext>(options =>options.UseSqlServer(setting.ConnectionStrings.SqlConnection));

        return services;
    }
}

