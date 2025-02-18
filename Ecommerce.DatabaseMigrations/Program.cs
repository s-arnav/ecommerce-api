using FluentMigrator.Runner;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Ecommerce.DatabaseMigrations;

public class Program
{
    public static void Main(string[] args)
    {
        using var scope = CreateServices().CreateScope();
        switch (args.FirstOrDefault())
        {
            case "-u":
                UpdateDatabase(scope.ServiceProvider);
                break;
            case "-d":
                if(args.Length < 2) throw new ArgumentNullException(nameof(args), "Missing version value");
                var version = args[1];
                RollbackDatabase(scope.ServiceProvider, long.Parse(version));
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(args), "Unknown argument");
        }
    }

    private static IConfiguration GetAppConfig()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
        
        var configuration = builder.Build();
        
        return configuration;
    }
    
    /// <summary>
    /// Configure DI services
    /// </summary>
    private static ServiceProvider CreateServices()
    {
        var connectionString = GetAppConfig().GetConnectionString("DbLocal");
        Log.Information("Connection String: {connectionString}", connectionString);
        
        return new ServiceCollection()
            .AddFluentMigratorCore()
            .ConfigureRunner(rb => rb.AddPostgres()
                .WithGlobalConnectionString(connectionString)
                .ScanIn(typeof(Program).Assembly).For.Migrations())
            .AddLogging(lb => lb.AddFluentMigratorConsole())
            .BuildServiceProvider(false);
    }

    /// <summary>
    /// Run migration Up
    /// </summary>
    /// <param name="serviceProvider"></param>
    private static void UpdateDatabase(IServiceProvider serviceProvider)
    {
        var runner = serviceProvider.GetRequiredService<IMigrationRunner>();
        runner.MigrateUp();
    }
    
    /// <summary>
    /// Run migration Down
    /// </summary>
    /// <param name="serviceProvider"></param>
    /// <param name="version"></param>
    private static void RollbackDatabase(IServiceProvider serviceProvider, long version)
    {
        var runner = serviceProvider.GetRequiredService<IMigrationRunner>();
        runner.MigrateDown(version);
    }
}