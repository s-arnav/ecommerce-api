using Ecommerce.Services.Dtos;
using Ecommerce.Services.Services;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryController(ICategoryService categoryService)
{
    [HttpGet("categories")]
    public async Task<IEnumerable<CategoryDto>> GetAllCategories()
    {
        return await categoryService.GetAllCategories();
    }
    
    [HttpGet("categories/{id}")]
    public async Task<CategoryDto?> GetAllCategories(Guid id)
    {
        return await categoryService.GetCategoryById(id);
    }
}