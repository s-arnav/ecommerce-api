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
        .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true, reloadOnChange: true)
        .Build();
    
    public string GetConnectionString() => Config.GetConnectionString("DbLocal") ?? "";
}