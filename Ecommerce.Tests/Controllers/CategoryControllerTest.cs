using System.ComponentModel.DataAnnotations;
using Ecommerce.Api.Rest.Controllers;
using Ecommerce.Services.ResponseDtos;
using Ecommerce.Services.Services;
using Ecommerce.Services.Utilities.Exceptions;
using Ecommerce.Tests.TestExtensions;
using Ecommerce.Tests.Utilities.Samples;
using Microsoft.AspNetCore.Http;
using Moq;
using Xunit;

namespace Ecommerce.Tests.Controllers;

public class CategoryControllerTest : BaseControllerTest
{
    private readonly Mock<ICategoryService> categoryService;
    private readonly CategoryController categoryController;
    
    public CategoryControllerTest()
    {
        categoryService = mockRepository.Create<ICategoryService>();
        categoryController = new CategoryController(categoryService.Object);
    }

    [Fact]
    public async Task ShouldGetAllCategories()
    {
        var categories = ResponseDtoSamples.Categories();
        
        categoryService.Setup(x => x.GetAllCategories()).ReturnsAsync(categories);
        
        var response = await categoryController.GetAllCategories();
        
        response.AssertSuccessResponse<IEnumerable<CategoryResponse>>(categories);
    }
    
    [Fact]
    public async Task GetAllCategoriesFailWithResponseStatus500()
    {
        categoryService.Setup(x => x.GetAllCategories()).ThrowsAsync(new Exception("Something went wrong"));
        
        var response = await categoryController.GetAllCategories();
        
        response.AssertFailureResponse<IEnumerable<CategoryResponse>>(StatusCodes.Status500InternalServerError);
    }

    [Fact]
    public async Task ShouldGetCategoryById()
    {
        var categoryDto = ResponseDtoSamples.Category;
        
        categoryService.Setup(x => x.GetCategoryById(categoryDto.Id)).ReturnsAsync(categoryDto);
        
        var response = await categoryController.GetCategory(categoryDto.Id);
        
        response.AssertSuccessResponse(categoryDto);
    }
    
    [Fact]
    public async Task GetCategoryByIdFailWhenNotFound()
    {
        var categoryId = Guid.NewGuid();
        
        categoryService.Setup(x => x.GetCategoryById(categoryId)).ThrowsAsync(new RecordNotFoundException());
        
        var response = await categoryController.GetCategory(categoryId);
        
        response.AssertFailureResponse<CategoryResponse>(StatusCodes.Status404NotFound);
    }
    
    [Fact]
    public async Task ShouldCreateCategory()
    {
        var request = RequestDtoSamples.CreateCategory;
        var expectedResponse = ResponseDtoSamples.Category;
        
        categoryService.Setup(x => x.CreateCategory(request)).ReturnsAsync(expectedResponse);
        
        var response = await categoryController.CreateCategory(request);
        
        response.AssertSuccessResponse(expectedResponse);
    }
    
    [Fact]
    public async Task CreateCategoryFailInvalidRequestBody()
    {
        var request = RequestDtoSamples.CreateCategory;
        request.Name = "";
        
        categoryService.Setup(x => x.CreateCategory(request))
            .ThrowsAsync(new ValidationAggregateException([new ValidationException("Invalid name")]));
        
        var response = await categoryController.CreateCategory(request);
        
        response.AssertFailureResponse<CategoryResponse>(StatusCodes.Status400BadRequest);
    }
    
    [Fact]
    public async Task ShouldUpdateCategory()
    {
        var request = RequestDtoSamples.UpdateCategory;
        var expectedResponse = ResponseDtoSamples.Category;
        expectedResponse.Id = request.Id;
        expectedResponse.Name = request.Name;
        expectedResponse.Description = request.Description;
        
        categoryService.Setup(x => x.UpdateCategory(request)).ReturnsAsync(expectedResponse);
        
        var response = await categoryController.UpdateCategory(request);
        
        response.AssertSuccessResponse(expectedResponse);
    }
    
    [Fact]
    public async Task UpdateCategoryFailInvalidRequestBody()
    {
        var categoryDto = RequestDtoSamples.UpdateCategory;
        categoryDto.Name = "";
        
        categoryService.Setup(x => x.UpdateCategory(categoryDto))
            .ThrowsAsync(new RecordNotFoundException());
        
        var response = await categoryController.UpdateCategory(categoryDto);
        
        response.AssertFailureResponse<CategoryResponse>(StatusCodes.Status404NotFound);
    }
    
    [Fact]
    public async Task ShouldDeleteCategory()
    {
        var categoryId = Guid.NewGuid();
        var expectedResponse = ResponseDtoSamples.Category;
        expectedResponse.Id = categoryId;
        
        categoryService.Setup(x => x.DeleteCategory(categoryId)).ReturnsAsync(expectedResponse);
        
        var response = await categoryController.DeleteCategory(categoryId);
        
        response.AssertSuccessResponse(expectedResponse);
    }
}