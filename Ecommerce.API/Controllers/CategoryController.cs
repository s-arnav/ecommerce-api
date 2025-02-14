using Ecommerce.API.Utilities;
using Ecommerce.Services.RequestDtos;
using Ecommerce.Services.Services;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers;

[Route("api/categories")]
public class CategoryController(ICategoryService categoryService) : BaseApiController
{
    [HttpGet]
    public async Task<IActionResult> GetAllCategories()
    {
        return await ExecuteReadOrUpdateAsync(categoryService.GetAllCategories);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCategory(Guid id)
    {
        return await ExecuteReadOrUpdateAsync(() => categoryService.GetCategoryById(id));
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryRequest categoryRequest)
    {
        return await ExecuteCreateAsync(() => categoryService.CreateCategory(categoryRequest));
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdateCategory([FromBody] UpdateCategoryRequest categoryRequest)
    {
        return await ExecuteReadOrUpdateAsync(() => categoryService.UpdateCategory(categoryRequest));
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory(Guid id)
    {
        return await ExecuteReadOrUpdateAsync(() => categoryService.DeleteCategory(id));
    }
}