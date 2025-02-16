using System.Reflection;
using Microsoft.Extensions.Configuration;

namespace Ecommerce.Services.Services;

public interface IAppConfigService
{
    public string GetConnectionString();
}

public class AppConfigService : IAppConfigService
{
    private static IConfigurationRoot Config => new ConfigurationBuilder()
        .SetBasePath(Path.GetDirectoryName(Assembly.GetCallingAssembly().Location)!)
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .Build();
    
    public string GetConnectionString() => Config.GetConnectionString("DbLocal") ?? "";
}