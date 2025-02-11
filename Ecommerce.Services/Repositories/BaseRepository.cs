using Dapper;
using Ecommerce.Services.Records;
using Ecommerce.Services.Utilities;
using Ecommerce.Services.Utilities.Config;
using Ecommerce.Services.Utilities.Exceptions;
using Npgsql;

namespace Ecommerce.Services.Repositories;

public interface IBaseRepository
{
    Task<IEnumerable<T>> GetAll<T>();
    Task<T> GetById<T>(Guid id) where T : BaseRecord;
    Task<Guid> Insert<T>(T record) where T : BaseRecord;
    Task Update<T>(T record) where T : BaseRecord;
    Task Delete(Guid id);
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

    public async Task<Guid> Insert<T>(T record) where T : BaseRecord
    {
        var sql = sqlBuilder.BuildInsertSql<T>(SchemaName, TableName, ["id"]);
        await using var connection = CreateConnection();
        var id = await connection.ExecuteScalarAsync<Guid?>(sql, record);

        if (!id.HasValue)
        {
            throw new Exception("Record not inserted");
        }
        
        return id.Value;
    }

    public async Task Update<T>(T record) where T : BaseRecord
    {
        var sql = sqlBuilder.BuildSimpleUpdateSql<T>(SchemaName, TableName, ["id", "created_on", "is_active", "is_deleted"]);
        await using var connection = CreateConnection();

        // Check if a record with the provided id exists
        await GetById<T>(record.id);
        
        var updatedRecordId = await connection.ExecuteScalarAsync<Guid?>(sql, record);
        
        if (!updatedRecordId.HasValue)
        {
            throw new Exception("Record not updated");
        }
    }
    
    public async Task Delete(Guid id)
    {
        var sql = sqlBuilder.BuildSoftDeleteSql(SchemaName, TableName);
        await using var connection = CreateConnection();

        // Check if a record with the provided id exists
        await GetById<BaseRecord>(id);
        
        var deletedRecordId = await connection.ExecuteScalarAsync<Guid?>(sql, new { id, updated_on = DateTime.UtcNow });
        
        if (!deletedRecordId.HasValue)
        {
            throw new Exception("Record not deleted");
        }
    }

    private NpgsqlConnection CreateConnection()
    {
        var connectionString = AppConfig.GetConnectionString();
        var connection = new NpgsqlConnection(connectionString);
        connection.Open();
        return connection;
    }
}
