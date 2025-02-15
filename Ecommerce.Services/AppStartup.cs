using Ecommerce.Services.Repositories;
using Ecommerce.Services.Services;
using Ecommerce.Services.Utilities;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Ecommerce.Services;

public static class AppStartup
{
    public static void ConfigureServices(IServiceCollection services)
    {
        // services
        services.AddSingleton<ISqlBuilder, SqlBuilder>();
        services.AddSingleton<ICategoryService, CategoryService>();
        
        // repositories
        services.AddSingleton<ICategoryRepository, CategoryRepository>();
    }

    public static void InitializeLogging()
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console()
            .CreateLogger();
    }
}