using System.Reflection;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Testcontainers.PostgreSql;
using Xunit;

namespace Ecommerce.Tests.Repositories.Fixtures;

public class DatabaseFixture : IAsyncLifetime
{
    private PostgreSqlContainer postgreSqlContainer;
    public string ConnectionString { get; private set; }

    public async ValueTask InitializeAsync()
    {
        postgreSqlContainer = new PostgreSqlBuilder().Build();
        await postgreSqlContainer.StartAsync();
        ConnectionString = postgreSqlContainer.GetConnectionString();
        RunMigrations(ConnectionString);
        Log.Information("{connectionString}", postgreSqlContainer.Id);
    }
    
    public ValueTask DisposeAsync() => postgreSqlContainer.DisposeAsync();
    
    private static void RunMigrations(string connectionString)
    {
        using var scope = new ServiceCollection()
            .AddFluentMigratorCore()
            .ConfigureRunner(rb => rb.AddPostgres()
                .WithGlobalConnectionString(connectionString)
                .ScanIn(Assembly.Load("Ecommerce.DatabaseMigrations")).For.Migrations())
            .AddLogging(lb => lb.AddFluentMigratorConsole())
            .BuildServiceProvider(false).CreateScope();
        
        var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
        runner.MigrateUp();
    }
}