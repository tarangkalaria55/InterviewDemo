using Mapster;
using Serilog;
using System.Reflection;
using WebApi.Auth;
using WebApi.Common;
using WebApi.Cors;
using WebApi.FluentValidation;
using WebApi.Logging.Serilog;
using WebApi.Middlewares;
using WebApi.Persistence;
using WebApi.Settings;
using WebApi.SignalR;
using WebApi.StaticFiles;
using WebApi.Swagger;

try
{
    StaticLogger.EnsureInitialized();
    Log.Information("Server Booting Up...");

    var builder = WebApplication.CreateBuilder(args);

    builder.RegisterSerilog();

    var assembly = Assembly.GetExecutingAssembly();

    // Mapster
    TypeAdapterConfig.GlobalSettings.Scan(assembly);

    // Add services to the container.

    builder.Services.AddSettings(builder.Configuration);

    builder.Services.AddSignals();

    builder.Services.AddPersistence();

    builder.Services.AddAuth();

    builder.Services.AddCorsPolicy();

    builder.Services.AddExceptionMiddleware();

    builder.Services.AddValidation(assembly);

    builder.Services.AddRouting(options => options.LowercaseUrls = true);

    builder.Services.AddServices(AppDomain.CurrentDomain);


    builder.Services.AddControllers();

    builder.Services.AddEndpointsApiExplorer();

    builder.Services.AddSwaggerSetting();


    var app = builder.Build();

    app.UseSerilogRequestLogging();

    app.UseSwaggerSetting();
    app.UseStaticFilesSetup();
    app.UseExceptionMiddleware();
    app.UseRouting();
    app.UseCorsPolicy();
    app.UseAuthentication();
    app.UseCurrentUser();
    app.UseAuthorization();

    app.UseHttpsRedirection();

    app.UseSignals();

    app.MapControllers().RequireAuthorization();

    app.Run();

}
catch (Exception ex) when (!ex.GetType().Name.Equals("StopTheHostException", StringComparison.Ordinal))
{
    StaticLogger.EnsureInitialized();
    Log.Fatal(ex, "Unhandled exception");
}
finally
{
    StaticLogger.EnsureInitialized();
    Log.Information("Server Shutting down...");
    Log.CloseAndFlush();
}


