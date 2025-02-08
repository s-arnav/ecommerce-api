using Ecommerce.Services.Dtos;
using Ecommerce.Services.Repositories;

namespace Ecommerce.Services.Services;

public interface ICategoryService
{
    Task<IEnumerable<CategoryDto>> GetAllCategories();
    Task<CategoryDto?> GetCategoryById(Guid id);
}

public class CategoryService(ICategoryRepository categoryRepository) : ICategoryService
{
    public async Task<IEnumerable<CategoryDto>> GetAllCategories()
    {
        var categories = await categoryRepository.GetAllCategories();

        return categories.Select(x => new CategoryDto { Id = x.id, Name = x.name, Description = x.description });
    }
    
    public async Task<CategoryDto?> GetCategoryById(Guid id)
    {
        var category = await categoryRepository.GetCategoryById(id);

        return category != null 
            ? new CategoryDto { Id = category.id, Name = category.name, Description = category.description } 
            : null;
    }
}