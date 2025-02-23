using Ecommerce.Services.Repositories;
using Ecommerce.Services.RequestDtos;
using Ecommerce.Services.ResponseDtos;
using Ecommerce.Services.Utilities.Extensions.Records;
using Ecommerce.Services.Utilities.Extensions.Requests;
using Ecommerce.Services.Utilities.Extensions.Validations;
using Ecommerce.Services.Utilities.Providers;
using Ecommerce.Services.Utilities.Validations;

namespace Ecommerce.Services.Services;

public interface ICategoryService
{
    Task<IEnumerable<CategoryResponse>> GetAllCategories();
    Task<CategoryResponse> GetCategoryById(Guid id);
    Task<CategoryResponse> CreateCategory(CreateCategoryRequest category);
    Task<CategoryResponse> UpdateCategory(UpdateCategoryRequest category);
    Task<CategoryResponse> DeleteCategory(Guid id);
}

public class CategoryService(ICategoryRepository categoryRepository, IDbConnectionProvider dbConnectionProvider) : ICategoryService
{
    public async Task<IEnumerable<CategoryResponse>> GetAllCategories()
    {
        var connection = dbConnectionProvider.CreateConnection();
        var categories = await categoryRepository.GetAllCategories(connection);

        return categories.ToResponse();
    }
    
    public async Task<CategoryResponse> GetCategoryById(Guid id)
    {
        Validation.Begin().IsValidId(id, nameof(id));
        
        var connection = dbConnectionProvider.CreateConnection();
        var category = await categoryRepository.GetCategoryById(id, connection);
        
        return category.ToResponse();
    }

    public async Task<CategoryResponse> CreateCategory(CreateCategoryRequest category)
    {
        category.Validate();
        
        var connection = dbConnectionProvider.CreateConnection();
        var createdCategory = await categoryRepository.CreateCategory(category.ToCreateRecord(), connection);
        
        return createdCategory.ToResponse();
    }

    public async Task<CategoryResponse> UpdateCategory(UpdateCategoryRequest category)
    {
        category.Validate();
        
        var connection = dbConnectionProvider.CreateConnection();
        var updatedCategory = await categoryRepository.UpdateCategory(category.ToUpdateRecord(), connection);
        
        return updatedCategory.ToResponse();
    }
    
    public async Task<CategoryResponse> DeleteCategory(Guid id)
    {
        Validation.Begin().IsValidId(id, nameof(id)).Check();
        
        var connection = dbConnectionProvider.CreateConnection();
        var deletedCategory = await categoryRepository.DeleteCategory(id, connection);

        return deletedCategory.ToResponse();
    }
}