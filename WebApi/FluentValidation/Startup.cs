using FluentValidation;
using FluentValidation.AspNetCore;
using System.Reflection;

namespace WebApi.FluentValidation;

public static class Startup
{
    public static void AddValidation(this IServiceCollection services, Assembly assembly)
    {
        services.AddFluentValidationClientsideAdapters();
        services.AddValidatorsFromAssembly(assembly);

    }
}
