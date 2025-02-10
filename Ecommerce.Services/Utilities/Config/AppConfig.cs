using System.Reflection;
using Microsoft.Extensions.Configuration;

namespace Ecommerce.Services.Utilities.Config;


public static class AppConfig
{
    private static IConfigurationRoot Config => new ConfigurationBuilder()
        .SetBasePath(Path.GetDirectoryName(Assembly.GetCallingAssembly().Location)!)
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .Build();
    
    public static string GetConnectionString() => Config.GetConnectionString("DbLocal") ?? "";
}