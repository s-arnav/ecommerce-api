using Ecommerce.Services.RequestDtos;
using Ecommerce.Services.Services;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Rest.Controllers;

[ApiController]
[Route("api/products")]
public class ProductController(IProductService productService) : BaseApiController
{
    [HttpGet]
    public async Task<IActionResult> GetAllProducts()
    {
        return await ExecuteReadOrUpdateAsync(productService.GetAllProducts);
    }
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetProduct(Guid id)
    {
        return await ExecuteReadOrUpdateAsync(() => productService.GetProductById(id));
    }
    
    [HttpGet("category/{categoryId:guid}")]
    public async Task<IActionResult> GetProductsForCategory(Guid categoryId)
    {
        return await ExecuteReadOrUpdateAsync(() => productService.GetProductsForCategory(categoryId));
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequest productRequest)
    {
        return await ExecuteCreateAsync(() => productService.CreateProduct(productRequest));
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductRequest productRequest)
    {
        return await ExecuteReadOrUpdateAsync(() => productService.UpdateProduct(productRequest));
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(Guid id)
    {
        return await ExecuteReadOrUpdateAsync(() => productService.DeleteProduct(id));
    }
}