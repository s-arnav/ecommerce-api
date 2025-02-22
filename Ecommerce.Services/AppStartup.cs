using Ecommerce.Services.Repositories;
using Ecommerce.Services.Services;
using Ecommerce.Services.Utilities;
using Ecommerce.Services.Utilities.Providers;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Ecommerce.Services;

public static class AppStartup
{
    public static void ConfigureServices(IServiceCollection services)
    {
        // services
        services.AddTransient<ISqlBuilder, SqlBuilder>();
        services.AddScoped<IAppConfigService, AppConfigService>();
        services.AddScoped<IDbConnectionProvider, DbConnectionProvider>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IDbQueryService, DbQueryService>();
        
        // repositories
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
    }

    public static void InitializeLogging()
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console()
            .CreateLogger();
    }
}