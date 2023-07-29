using Microsoft.OpenApi.Models;
using WebApi.Settings;

namespace WebApi.Swagger;

public static class Startup
{
    public static IServiceCollection AddSwaggerSetting(this IServiceCollection services)
    {
        var setting = services.BuildServiceProvider().GetRequiredService<AppSetting>();
        var swaggerSetting = setting.SwaggerSetting;

        var title = string.IsNullOrWhiteSpace(swaggerSetting.Title) ? "Web API" : swaggerSetting.Title;
        var version = string.IsNullOrWhiteSpace(swaggerSetting.Version) ? "v1" : swaggerSetting.Version;

        services.AddSwaggerGen(option =>
        {
            option.SwaggerDoc(version, new OpenApiInfo { Title = title, Version = version });
            option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter a valid token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "Bearer"
            });
            option.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type=ReferenceType.SecurityScheme,
                            Id="Bearer"
                        }
                    },
                    new string[]{}
                }
            });
        });

        return services;
    }

    public static IApplicationBuilder UseSwaggerSetting(this IApplicationBuilder app)
    {
        var setting = app.ApplicationServices.GetRequiredService<AppSetting>();
        var swaggerSetting = setting.SwaggerSetting;

        if (swaggerSetting.EnableSwagger)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        return app;
    }
}
