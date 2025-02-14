using Ecommerce.Services.Repositories;
using Ecommerce.Services.RequestDtos;
using Ecommerce.Services.ResponseDtos;
using Ecommerce.Services.Utilities.Extensions.Records;
using Ecommerce.Services.Utilities.Extensions.Requests;
using Ecommerce.Services.Utilities.Extensions.Validations;
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

public class CategoryService(ICategoryRepository categoryRepository) : ICategoryService
{
    public async Task<IEnumerable<CategoryResponse>> GetAllCategories()
    {
        var categories = await categoryRepository.GetAllCategories();

        return categories.Select(category => category.ToResponse());
    }
    
    public async Task<CategoryResponse> GetCategoryById(Guid id)
    {
        Validation.Begin().IsValidId(id, nameof(id));
        
        var category = await categoryRepository.GetCategoryById(id);
        
        return category.ToResponse();
    }

    public async Task<CategoryResponse> CreateCategory(CreateCategoryRequest category)
    {
        category.ValidateCreateCategoryRequest();
        
        var createdCategory = await categoryRepository.CreateCategory(category.ToCreateRecord());
        
        return createdCategory.ToResponse();
    }

    public async Task<CategoryResponse> UpdateCategory(UpdateCategoryRequest category)
    {
        category.ValidateUpdateCategoryRequest();
        
        var updatedCategory = await categoryRepository.UpdateCategory(category.ToUpdateRecord());
        
        return updatedCategory.ToResponse();
    }
    
    public async Task<CategoryResponse> DeleteCategory(Guid id)
    {
        Validation.Begin().IsValidId(id, nameof(id)).Check();
        
        var deletedCategory = await categoryRepository.DeleteCategory(id);

        return deletedCategory.ToResponse();
    }
}