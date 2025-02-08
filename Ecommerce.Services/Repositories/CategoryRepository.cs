using Ecommerce.Services.Constants;
using Ecommerce.Services.Records;

namespace Ecommerce.Services.Repositories;

public interface ICategoryRepository
{
    Task<IEnumerable<CategoryRecord>> GetAllCategories();
    Task<CategoryRecord?> GetCategoryById(Guid id);
}

public class CategoryRepository : BaseRepository, ICategoryRepository
{
    protected override string SchemaName => Schemas.Ops;
    protected override string TableName => OpsTables.Category;


    public async Task<IEnumerable<CategoryRecord>> GetAllCategories() => await GetAll<CategoryRecord>();

    public async Task<CategoryRecord?> GetCategoryById(Guid id) => await GetById<CategoryRecord>(id);
}