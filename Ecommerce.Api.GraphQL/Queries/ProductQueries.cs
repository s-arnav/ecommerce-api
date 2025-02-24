using Ecommerce.Services.ResponseDtos;
using Ecommerce.Services.Services;

namespace Ecommerce.Api.GraphQL.Queries;

[ExtendObjectType(typeof(Query))]
public class ProductQueries
{
    public async Task<IEnumerable<ProductResponse>> GetProducts(IProductService productService) =>
        await productService.GetAllProducts();

    public async Task<ProductResponse> GetProductById(IProductService productService, Guid id) =>
        await productService.GetProductById(id);

    public async Task<IEnumerable<ProductResponse>> GetProductsByCategoryId(IProductService productService, Guid categoryId)
        => await productService.GetProductsForCategory(categoryId);
}