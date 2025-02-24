using Ecommerce.Services.ResponseDtos;
using Ecommerce.Services.Services;

namespace Ecommerce.Api.GraphQL.Queries;

[ExtendObjectType(typeof(Query))]
public class CategoryQueries
{
    public async Task<IEnumerable<CategoryResponse>> GetCategories(ICategoryService categoryService) =>
        await categoryService.GetAllCategories();

    public async Task<CategoryResponse> GetCategoryById(ICategoryService categoryService, Guid id) =>
        await categoryService.GetCategoryById(id);
}