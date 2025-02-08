using System.Data;
using Dapper;
using Npgsql;

namespace Ecommerce.Services.Repositories;

public interface IBaseRepository
{
    Task<IEnumerable<T>> GetAll<T>();
    Task<T?> GetById<T>(Guid id);
}

public abstract class BaseRepository : IBaseRepository
{
    protected virtual string SchemaName { get; set; } = "";
    protected virtual string TableName { get; set; } = "";
    
    public async Task<IEnumerable<T>> GetAll<T>()
    {
        try
        {
            await using var connection = CreateConnection();
            return await connection.QueryAsync<T>($@"SELECT * FROM {SchemaName}.{TableName}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{nameof(GetAll)}: {ex.Message}");
            return new List<T>();
        }
    }
    
    public async Task<T?> GetById<T>(Guid id)
    {
        try
        {
            await using var connection = CreateConnection();
            return await connection.QuerySingleOrDefaultAsync<T>($@"SELECT * FROM {SchemaName}.{TableName} WHERE id = @id", new { id });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{nameof(GetById)}: {ex.Message}");
            return default;
        }
    }

    private static NpgsqlConnection CreateConnection()
    {
        var connection = new NpgsqlConnection("Server=127.0.0.1;Port=5433;Userid=root;Password=root;Database=ecommerce;");
        connection.Open();
        return connection;
    }
}
