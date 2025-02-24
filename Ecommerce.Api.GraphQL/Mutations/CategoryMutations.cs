using Ecommerce.Services.RequestDtos;
using Ecommerce.Services.ResponseDtos;
using Ecommerce.Services.Services;

namespace Ecommerce.Api.GraphQL.Mutations;

[ExtendObjectType(typeof(Mutation))]
public class CategoryMutations
{
    public async Task<CategoryResponse> CreateCategory(ICategoryService categoryService, CreateCategoryRequest category)
        => await categoryService.CreateCategory(category);
    
    public async Task<CategoryResponse> UpdateCategory(ICategoryService categoryService, UpdateCategoryRequest category)
        => await categoryService.UpdateCategory(category);
    
    public async Task<CategoryResponse> DeleteCategory(ICategoryService categoryService, Guid id)
        => await categoryService.DeleteCategory(id);
}