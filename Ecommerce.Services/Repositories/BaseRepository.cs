using System.Data;
using Ecommerce.Services.Records;
using Ecommerce.Services.Utilities;
using Ecommerce.Services.Utilities.Exceptions;
using Ecommerce.Services.Utilities.Extensions.Generic;
using Serilog;

namespace Ecommerce.Services.Repositories;

public abstract class BaseRepository(ISqlBuilder sqlBuilder, IDbQueryService dbQueryService)
{
    protected virtual string SchemaName => "";
    protected virtual string TableName => "";
    
    protected async Task<IEnumerable<T>> GetAll<T>(IDbConnection connection)
    {
        var sql = sqlBuilder.BuildGetAllSql(SchemaName, TableName);

        return await dbQueryService.QueryAsync<T>(sql, new {}, connection);
    }
    
    protected async Task<T> GetById<T>(Guid id, IDbConnection connection) where T : BaseRecord
    {
        var sql = sqlBuilder.BuildGetByIdSql(SchemaName, TableName);
        
        var record = await dbQueryService.QuerySingleOrDefaultAsync<T>(sql, new { id }, connection);

        if (record == null)
        {
            throw new RecordNotFoundException($"{typeof(T).Name}: No record found for id: '{id}'");
        }
        
        return record;
    }

    protected async Task<T> Insert<T>(T record, IDbConnection connection) where T : BaseRecord
    {
        var sql = sqlBuilder.BuildInsertSql<T>(SchemaName, TableName, [nameof(record.id)]);

        var createdRecord = await dbQueryService.QuerySingleOrDefaultAsync<T>(sql, record, connection);

        if (createdRecord == null)
        {
            throw new Exception($"{typeof(T).Name}: Record not inserted");
        }
        
        return createdRecord;
    }

    protected async Task<T> Update<T>(T record, IDbConnection connection) where T : BaseRecord, new()
    {
        var sql = sqlBuilder.BuildSimpleUpdateSql<T>(SchemaName, TableName,
            [nameof(record.id), nameof(record.created_on), nameof(record.is_deleted)]);

        var updatedRecord = await dbQueryService.QuerySingleOrDefaultAsync<T>(sql, record, connection);
        
        if (updatedRecord == null)
        {
            throw new RecordNotFoundException($"{typeof(T).Name}: No record found for id: '{record.id}'");
        }
        
        return updatedRecord;
    }
    
    protected async Task<T> Delete<T>(Guid id, IDbConnection connection) where T : BaseRecord
    {
        var sql = sqlBuilder.BuildSoftDeleteSql(SchemaName, TableName);

        var deletedRecord = await dbQueryService.QuerySingleOrDefaultAsync<T>(sql, new { id, updated_on = DateTime.UtcNow }, connection);
        
        if (deletedRecord == null)
        {
            throw new RecordNotFoundException($"{typeof(T).Name}: No record found for id: '{id}'");
        }
        
        return deletedRecord;
    }
    
    private async Task<T> MergeRecords<T>(T record, IDbConnection connection, string[]? fieldsToIgnore = null) where T : BaseRecord, new()
    {
        T existingRecord;

        try
        {
            existingRecord = await GetById<T>(record.id, connection);
            Log.Debug("Existing: {existingRecord}", record.ToJson());
        }
        catch
        {
            return record;
        }

        var recordColumns = typeof(T).GetProperties().Select(x => x.Name)
            .Except(fieldsToIgnore ?? [nameof(record.id), nameof(record.created_on), nameof(record.is_deleted)])
            .ToList();

        var mergedRecord = new T { id = existingRecord.id, created_on = existingRecord.created_on, is_deleted = existingRecord.is_deleted };

        foreach (var column in recordColumns)
        {
            var incomingValue = record.GetType().GetProperty(column)?.GetValue(record);
            var existingValue = existingRecord.GetType().GetProperty(column)?.GetValue(existingRecord);
            mergedRecord.GetType().GetProperty(column)?.SetValue(mergedRecord,
                incomingValue != null || incomingValue as string != "" ? incomingValue : existingValue);
        }

        return mergedRecord;
    }
    
    protected async Task<IEnumerable<T>> QueryAsync<T>(string sql, object param, IDbConnection connection)
        => await dbQueryService.QueryAsync<T>(sql, param, connection);
    
    protected async Task<T?> QuerySingleOrDefaultAsync<T>(string sql, object param, IDbConnection connection)
        => await dbQueryService.QuerySingleOrDefaultAsync<T>(sql, param, connection);
}
