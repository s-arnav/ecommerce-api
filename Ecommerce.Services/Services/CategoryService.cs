using Ecommerce.Services.Dtos;
using Ecommerce.Services.Records;
using Ecommerce.Services.Repositories;
using Ecommerce.Services.Utilities.Exceptions;
using Ecommerce.Services.Utilities.Validations;

namespace Ecommerce.Services.Services;

public interface ICategoryService
{
    Task<IEnumerable<CategoryDto>> GetAllCategories();
    Task<CategoryDto> GetCategoryById(Guid id);
    Task<CategoryDto> CreateCategory(CategoryDto categoryDto);
    Task<CategoryDto> UpdateCategory(CategoryDto categoryDto);
}

public class CategoryService(ICategoryRepository categoryRepository) : ICategoryService
{
    public async Task<IEnumerable<CategoryDto>> GetAllCategories()
    {
        var categories = await categoryRepository.GetAllCategories();

        return categories.Select(x => new CategoryDto { Id = x.id, Name = x.name, Description = x.description });
    }
    
    public async Task<CategoryDto> GetCategoryById(Guid id)
    {
        Validation.Begin().IsValidId(id, nameof(id));
        
        var category = await categoryRepository.GetCategoryById(id);
        
        if (category == null) throw new RecordNotFoundException("Category not found");

        return new CategoryDto { Id = category.id, Name = category.name, Description = category.description };
    }

    public async Task<CategoryDto> CreateCategory(CategoryDto categoryDto)
    {
        Validation.Begin()
            .IsNotNullOrEmptyString(categoryDto.Name, nameof(categoryDto.Name))
            .IsNotNullOrEmptyString(categoryDto.Description, nameof(categoryDto.Description));
        
        var categoryId = await categoryRepository.CreateCategory(new CategoryRecord { name = categoryDto.Name, description = categoryDto.Description });
        
        return new CategoryDto { Id = categoryId, Name = categoryDto.Name, Description = categoryDto.Description };
    }

    public async Task<CategoryDto> UpdateCategory(CategoryDto categoryDto)
    {
        Validation.Begin()
            .IsNotNullOrEmptyString(categoryDto.Name, nameof(categoryDto.Name))
            .IsNotNullOrEmptyString(categoryDto.Description, nameof(categoryDto.Description));
        
        var categoryId = await categoryRepository.UpdateCategory(new CategoryRecord { id = categoryDto.Id, name = categoryDto.Name, description = categoryDto.Description });
        
        return new CategoryDto { Id = categoryId, Name = categoryDto.Name, Description = categoryDto.Description };
    }
}