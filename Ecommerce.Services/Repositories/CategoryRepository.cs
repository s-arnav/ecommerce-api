using Ecommerce.Services.Constants;
using Ecommerce.Services.Records;
using Ecommerce.Services.Utilities;

namespace Ecommerce.Services.Repositories;

public interface ICategoryRepository
{
    Task<IEnumerable<CategoryRecord>> GetAllCategories();
    Task<CategoryRecord> GetCategoryById(Guid id);
    Task<CategoryRecord> CreateCategory(CategoryRecord category);
    Task<CategoryRecord> UpdateCategory(CategoryRecord categoryRecord);
    Task<CategoryRecord> DeleteCategory(Guid id);
}

public class CategoryRepository(ISqlBuilder sqlBuilder) : BaseRepository(sqlBuilder), ICategoryRepository
{
    protected override string SchemaName => Schemas.Ops;
    protected override string TableName => OpsTables.Category;

    public async Task<IEnumerable<CategoryRecord>> GetAllCategories() => await GetAll<CategoryRecord>();

    public async Task<CategoryRecord> GetCategoryById(Guid id) => await GetById<CategoryRecord>(id);

    public async Task<CategoryRecord> CreateCategory(CategoryRecord categoryRecord) => await Insert(categoryRecord);

    public async Task<CategoryRecord> UpdateCategory(CategoryRecord categoryRecord) => await Update(categoryRecord);
    
    public async Task<CategoryRecord> DeleteCategory(Guid id) => await Delete<CategoryRecord>(id);
}