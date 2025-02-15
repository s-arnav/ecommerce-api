using Ecommerce.Services.RequestDtos;
using Ecommerce.Services.ResponseDtos;
using Ecommerce.Services.Services;

namespace Ecommerce.Api.GraphQL.Mutations;

[ExtendObjectType(typeof(Mutation))]
public class CategoryMutations
{
    public async Task<CategoryResponse> CreateCategory(CreateCategoryRequest input, ICategoryService categoryService)
        => await categoryService.CreateCategory(input);
    
    public async Task<CategoryResponse> UpdateCategory(UpdateCategoryRequest input, ICategoryService categoryService)
        => await categoryService.UpdateCategory(input);
    
    public async Task<CategoryResponse> DeleteCategory(Guid id, ICategoryService categoryService)
        => await categoryService.DeleteCategory(id);
}