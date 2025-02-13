using Dapper;
using Ecommerce.Services.Records;
using Ecommerce.Services.Utilities;
using Ecommerce.Services.Utilities.Config;
using Ecommerce.Services.Utilities.Exceptions;
using Npgsql;
using Serilog;

namespace Ecommerce.Services.Repositories;

public interface IBaseRepository
{
    Task<IEnumerable<T>> GetAll<T>();
    Task<T> GetById<T>(Guid id) where T : BaseRecord;
    Task<T> Insert<T>(T record) where T : BaseRecord;
    Task<T> Update<T>(T record) where T : BaseRecord;
    Task<T> Delete<T>(Guid id) where T : BaseRecord;
}

public abstract class BaseRepository(ISqlBuilder sqlBuilder) : IBaseRepository
{
    protected virtual string SchemaName { get; set; } = "";
    protected virtual string TableName { get; set; } = "";
    
    public async Task<IEnumerable<T>> GetAll<T>()
    {
        var sql = sqlBuilder.BuildGetAllSql(SchemaName, TableName);
        await using var connection = CreateConnection();
        
        return await connection.QueryAsync<T>(sql);
    }
    
    public async Task<T> GetById<T>(Guid id) where T : BaseRecord
    {
        var sql = sqlBuilder.BuildGetByIdSql(SchemaName, TableName);
        await using var connection = CreateConnection();
        
        var record = await connection.QuerySingleOrDefaultAsync<T>(sql, new { id });

        if (record == null)
        {
            throw new RecordNotFoundException($"No record found for id: {id}");
        }
        
        return record;
    }

    public async Task<T> Insert<T>(T record) where T : BaseRecord
    {
        var sql = sqlBuilder.BuildInsertSql<T>(SchemaName, TableName, ["id"]);
        await using var connection = CreateConnection();
        
        var createdRecord = await connection.QueryFirstOrDefaultAsync<T>(sql, record);

        if (createdRecord == null)
        {
            throw new Exception("Record not inserted");
        }
        
        return createdRecord;
    }

    public async Task<T> Update<T>(T record) where T : BaseRecord
    {
        var sql = sqlBuilder.BuildSimpleUpdateSql<T>(SchemaName, TableName, ["id", "created_on", "is_deleted"]);
        await using var connection = CreateConnection();
        
        // Check if a record with the provided id exists
        await GetById<T>(record.id);
        
        var updatedRecord = await connection.QueryFirstOrDefaultAsync<T>(sql, record);
        
        if (updatedRecord == null)
        {
            throw new Exception("Record not updated");
        }
        
        return updatedRecord;
    }
    
    public async Task<T> Delete<T>(Guid id) where T : BaseRecord
    {
        var sql = sqlBuilder.BuildSoftDeleteSql(SchemaName, TableName);
        await using var connection = CreateConnection();
        
        // Check if a record with the provided id exists
        await GetById<BaseRecord>(id);
        
        var deletedRecord = await connection.QueryFirstOrDefaultAsync<T>(sql, new { id, updated_on = DateTime.UtcNow });
        
        if (deletedRecord == null)
        {
            throw new Exception("Record not deleted");
        }
        
        return deletedRecord;
    }

    private static NpgsqlConnection CreateConnection()
    {
        var connectionString = AppConfig.GetConnectionString();
        Log.Debug("Creating Npgsql connection: {connectionString}", connectionString);
        
        var connection = new NpgsqlConnection(connectionString);
        connection.Open();
        
        return connection;
    }
}
