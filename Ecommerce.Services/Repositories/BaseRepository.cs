using Dapper;
using Ecommerce.Services.Records;
using Ecommerce.Services.Utilities;
using Ecommerce.Services.Utilities.Exceptions;
using Ecommerce.Services.Utilities.Providers;

namespace Ecommerce.Services.Repositories;

public abstract class BaseRepository(ISqlBuilder sqlBuilder, IDbConnectionProvider dbConnectionProvider)
{
    protected virtual string SchemaName => "";
    protected virtual string TableName => "";
    
    public async Task<IEnumerable<T>> GetAll<T>()
    {
        var sql = sqlBuilder.BuildGetAllSql(SchemaName, TableName);
        using var connection = dbConnectionProvider.CreateConnection();
        
        return await connection.QueryAsync<T>(sql);
    }
    
    public virtual async Task<T> GetById<T>(Guid id) where T : BaseRecord
    {
        var sql = sqlBuilder.BuildGetByIdSql(SchemaName, TableName);
        using var connection = dbConnectionProvider.CreateConnection();
        
        var record = await connection.QuerySingleOrDefaultAsync<T>(sql, new { id });

        if (record == null)
        {
            throw new RecordNotFoundException($"No record found for id: '{id}'");
        }
        
        return record;
    }

    public async Task<T> Insert<T>(T record) where T : BaseRecord
    {
        var sql = sqlBuilder.BuildInsertSql<T>(SchemaName, TableName, [nameof(record.id)]);
        using var connection = dbConnectionProvider.CreateConnection();
        
        var createdRecord = await connection.QueryFirstOrDefaultAsync<T>(sql, record);

        if (createdRecord == null)
        {
            throw new Exception("Record not inserted");
        }
        
        return createdRecord;
    }

    public async Task<T> Update<T>(T record) where T : BaseRecord
    {
        var sql = sqlBuilder.BuildSimpleUpdateSql<T>(SchemaName, TableName,
            [nameof(record.id), nameof(record.created_on), nameof(record.is_deleted)]);
        using var connection = dbConnectionProvider.CreateConnection();
        
        var updatedRecord = await connection.QueryFirstOrDefaultAsync<T>(sql, record);
        
        if (updatedRecord == null)
        {
            throw new RecordNotFoundException($"No record found for id: '{record.id}'");
        }
        
        return updatedRecord;
    }
    
    public async Task<T> Delete<T>(Guid id) where T : BaseRecord
    {
        var sql = sqlBuilder.BuildSoftDeleteSql(SchemaName, TableName);
        using var connection = dbConnectionProvider.CreateConnection();
        
        var deletedRecord = await connection.QueryFirstOrDefaultAsync<T>(sql, new { id, updated_on = DateTime.UtcNow });
        
        if (deletedRecord == null)
        {
            throw new Exception($"No record found for id: '{id}'");
        }
        
        return deletedRecord;
    }

    public async Task<T> MergeRecords<T>(T record, string[]? fieldsToIgnore = null) where T : BaseRecord, new()
    {
        T existingRecord;

        try
        {
            existingRecord = await GetById<T>(record.id);
        }
        catch
        {
            return record;
        }

        var recordColumns =  typeof(T).GetProperties().Select(x => x.Name).Except(fieldsToIgnore ?? []).ToList();

        var mergedRecord = new T();

        foreach (var column in recordColumns)
        {
            var incomingValue = record.GetType().GetProperty(column)?.GetValue(record);
            var existingValue = existingRecord.GetType().GetProperty(column)?.GetValue(existingRecord);
            mergedRecord.GetType().GetProperty(column)?.SetValue(mergedRecord,
                incomingValue != null || incomingValue as string != "" ? incomingValue : existingValue);
        }

        return mergedRecord;
    }
}
