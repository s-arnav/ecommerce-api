using Ecommerce.Services.RequestDtos;
using Ecommerce.Services.ResponseDtos;
using Ecommerce.Services.Services;

namespace Ecommerce.Api.GraphQL.Mutations;

[ExtendObjectType(typeof(Mutation))]
public class CategoryMutations
{
    public async Task<CategoryResponse> CreateCategory(CreateCategoryRequest category, ICategoryService categoryService)
        => await categoryService.CreateCategory(category);
    
    public async Task<CategoryResponse> UpdateCategory(UpdateCategoryRequest category, ICategoryService categoryService)
        => await categoryService.UpdateCategory(category);
    
    public async Task<CategoryResponse> DeleteCategory(Guid id, ICategoryService categoryService)
        => await categoryService.DeleteCategory(id);
}