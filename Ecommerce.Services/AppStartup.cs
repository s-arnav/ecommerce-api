using Ecommerce.Services.Repositories;
using Ecommerce.Services.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Services;

public static class AppStartup
{
    public static void ConfigureServices(IServiceCollection services)
    {
        // services
        services.AddTransient<ICategoryService, CategoryService>();
        
        // repositories
        services.AddTransient<ICategoryRepository, CategoryRepository>();
    }
}