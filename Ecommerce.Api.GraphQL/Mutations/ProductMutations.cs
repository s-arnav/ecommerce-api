using Ecommerce.Services.RequestDtos;
using Ecommerce.Services.ResponseDtos;
using Ecommerce.Services.Services;

namespace Ecommerce.Api.GraphQL.Mutations;

[ExtendObjectType(typeof(Mutation))]
public class ProductMutations
{
    public async Task<ProductResponse> CreateProduct(IProductService productService, CreateProductRequest product)
        => await productService.CreateProduct(product);
    
    public async Task<ProductResponse> UpdateProduct(IProductService productService, UpdateProductRequest product)
        => await productService.UpdateProduct(product);
    
    public async Task<ProductResponse> DeleteCategory(IProductService productService, Guid id)
        => await productService.DeleteProduct(id);
}