using System.Data;
using Ecommerce.Services.Constants;
using Ecommerce.Services.Records;
using Ecommerce.Services.Utilities;

namespace Ecommerce.Services.Repositories;

public interface ICategoryRepository
{
    Task<IEnumerable<CategoryRecord>> GetAllCategories(IDbConnection connection);
    Task<CategoryRecord> GetCategoryById(Guid id, IDbConnection connection);
    Task<CategoryRecord> CreateCategory(CategoryRecord categoryRecord, IDbConnection connection);
    Task<CategoryRecord> UpdateCategory(CategoryRecord categoryRecord, IDbConnection connection);
    Task<CategoryRecord> DeleteCategory(Guid id, IDbConnection connection);
}

public class CategoryRepository(ISqlBuilder sqlBuilder, IDbQueryService dbQueryService)
    : BaseRepository(sqlBuilder, dbQueryService), ICategoryRepository
{
    protected override string SchemaName => Schemas.Ops;
    protected override string TableName => OpsTables.Category;

    public async Task<IEnumerable<CategoryRecord>> GetAllCategories(IDbConnection connection) => await GetAll<CategoryRecord>(connection);

    public async Task<CategoryRecord> GetCategoryById(Guid id, IDbConnection connection) => await GetById<CategoryRecord>(id, connection);

    public async Task<CategoryRecord> CreateCategory(CategoryRecord categoryRecord, IDbConnection connection) => await Insert(categoryRecord, connection);

    public async Task<CategoryRecord> UpdateCategory(CategoryRecord categoryRecord, IDbConnection connection) => await Update(categoryRecord, connection);
    
    public async Task<CategoryRecord> DeleteCategory(Guid id, IDbConnection connection) => await Delete<CategoryRecord>(id, connection);
}