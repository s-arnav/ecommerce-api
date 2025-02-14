namespace Ecommerce.Services.Utilities;

public interface ISqlBuilder
{
    string BuildGetAllSql(string schemaName, string tableName);
    string BuildGetByIdSql(string schemaName, string tableName);
    string BuildInsertSql<T>(string schemaName, string tableName, string[] fieldsToIgnore) where T : class;
    string BuildSimpleUpdateSql<T>(string schemaName, string tableName, string[] fieldsToIgnore) where T : class;
    string BuildSoftDeleteSql(string schemaName, string tableName);
}

public class SqlBuilder : ISqlBuilder
{
    public string BuildGetAllSql(string schemaName, string tableName)
    {
        return $"SELECT * FROM {schemaName}.{tableName}";
    }

    public string BuildGetByIdSql(string schemaName, string tableName)
    {
        return $"SELECT * FROM {schemaName}.{tableName} WHERE id = @id";
    }

    public string BuildInsertSql<T>(string schemaName, string tableName, string[] fieldsToIgnore) where T : class
    {
        var fields =  typeof(T).GetProperties().Select(x => x.Name).Where(x => !fieldsToIgnore.Contains(x.ToLower())).ToList();
        var values = fields.Select(x => $"@{x}");

        var fieldsString = string.Join(", ", fields);
        var valuesString = string.Join(", ", values);

        return $"INSERT INTO {schemaName}.{tableName} ({fieldsString}) VALUES ({valuesString}) RETURNING *";
    }

    public string BuildSimpleUpdateSql<T>(string schemaName, string tableName, string[] fieldsToIgnore) where T : class
    {
        var fields =  typeof(T).GetProperties().Select(x => x.Name).Where(x => !fieldsToIgnore.Contains(x.ToLower())).ToList();
        var values = fields.Select(x => $"{x} = @{x}");

        var valuesString = string.Join(", ", values);

        return $"UPDATE {schemaName}.{tableName} SET {valuesString} WHERE id = @id RETURNING *";
    }

    public string BuildSoftDeleteSql(string schemaName, string tableName)
    {
        return $"UPDATE {schemaName}.{tableName} SET is_active = false, is_deleted = true, updated_on = @updated_on WHERE id = @id RETURNING *";
    }
}