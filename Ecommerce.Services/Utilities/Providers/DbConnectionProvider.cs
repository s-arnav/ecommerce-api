using System.Data;
using Ecommerce.Services.Services;
using Npgsql;
using Serilog;

namespace Ecommerce.Services.Utilities.Providers;

public interface IDbConnectionProvider
{
    IDbConnection CreateConnection();
}

public class DbConnectionProvider(IAppConfigService appConfigService) : IDbConnectionProvider
{
    public IDbConnection CreateConnection()
    {
        var connectionString = appConfigService.GetConnectionString();
        Log.Debug("Creating Npgsql connection: {connectionString}", connectionString);
        
        var connection = new NpgsqlConnection(connectionString);
        connection.Open();
        
        return connection;
    }
}