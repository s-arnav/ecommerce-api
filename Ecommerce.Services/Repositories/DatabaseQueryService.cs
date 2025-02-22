using System.Data;
using Dapper;

namespace Ecommerce.Services.Repositories;

public interface IDbQueryService
{
    Task<IEnumerable<T>> QueryAsync<T>(string sql, object param, IDbConnection connection);
    Task<T?> QuerySingleOrDefaultAsync<T>(string sql, object param, IDbConnection connection);
}

public class DbQueryService : IDbQueryService
{
    public Task<IEnumerable<T>> QueryAsync<T>(string sql, object param, IDbConnection connection)
    {
        return connection.QueryAsync<T>(sql, param);
    }

    public Task<T?> QuerySingleOrDefaultAsync<T>(string sql, object param, IDbConnection connection)
    {
        return connection.QuerySingleOrDefaultAsync<T>(sql, param);
    }
}