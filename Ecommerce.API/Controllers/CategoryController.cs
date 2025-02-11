using Ecommerce.API.Utilities;
using Ecommerce.Services.Dtos;
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
    public async Task<IActionResult> GetAllCategories(Guid id)
    {
        return await ExecuteReadOrUpdateAsync(() => categoryService.GetCategoryById(id));
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateCategory(CategoryDto categoryDto)
    {
        return await ExecuteCreateAsync(() => categoryService.CreateCategory(categoryDto));
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdateCategory(CategoryDto categoryDto)
    {
        return await ExecuteReadOrUpdateAsync(() => categoryService.UpdateCategory(categoryDto));
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> UpdateCategory(Guid id)
    {
        return await ExecuteReadOrUpdateAsync(() => categoryService.DeleteCategory(id));
    }
}