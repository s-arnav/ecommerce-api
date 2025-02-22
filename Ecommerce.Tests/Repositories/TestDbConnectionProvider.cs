using System.Data;
using Ecommerce.Services.Utilities.Providers;
using Npgsql;
using Serilog;

namespace Ecommerce.Tests.Repositories;

public class TestDbConnectionProvider(string connectionString) : IDbConnectionProvider
{
    public IDbConnection CreateConnection()
    {
        Log.Debug("Creating Npgsql connection: {connectionString}", connectionString);
        
        var connection = new NpgsqlConnection(connectionString);
        connection.Open();
        
        return connection;
    }
}