using System.Text.Json;
using Ecommerce.Services.Records;
using Ecommerce.Services.Repositories;
using Ecommerce.Services.Services;
using Ecommerce.Services.Utilities.Extensions.Records;
using Ecommerce.Services.Utilities.Extensions.Requests;
using Ecommerce.Tests.Utilities.Samples;
using Moq;
using Shouldly;

namespace Ecommerce.Tests.Services;

public class CategoryServiceTest : IDisposable
{
    private readonly CategoryService categoryService;
    private readonly Mock<ICategoryRepository> categoryRepository;

    public CategoryServiceTest()
    {
        categoryRepository = new Mock<ICategoryRepository>();
        categoryService = new CategoryService(categoryRepository.Object);
    }

    public void Dispose()
    {
        categoryRepository.VerifyAll();
    }

    [Test]
    public async Task ShouldGetAllCategories()
    {
        var records = RecordSamples.Categories();
        var expectedResponse = records.Select(x => x.ToResponse());

        categoryRepository.Setup(x => x.GetAllCategories()).ReturnsAsync(records);

        var response = await categoryService.GetAllCategories();
        response.ShouldBeEquivalentTo(expectedResponse);
    }

    [Test]
    public async Task ShouldGetCategory()
    {
        var id = Guid.NewGuid();
        var record = RecordSamples.Category;
        record.id = id;
        var expectedResponse = record.ToResponse();

        categoryRepository.Setup(x => x.GetCategoryById(id)).ReturnsAsync(record);

        var response = await categoryService.GetCategoryById(id);
        response.ShouldBeEquivalentTo(expectedResponse);
    }

    [Test]
    public async Task ShouldCreateCategory()
    {
        var request = RequestDtoSamples.CreateCategory;
        var record = new CategoryRecord { id = Guid.NewGuid(), name = request.Name, description = request.Description };
        var expectedResponse = record.ToResponse();

        categoryRepository
            .Setup(x => x.CreateCategory(It.Is<CategoryRecord>(y =>
                y.name == request.Name && y.description == request.Description))).ReturnsAsync(record);

        var response = await categoryService.CreateCategory(request);
        response.ShouldBeEquivalentTo(expectedResponse);
    }


    [Test]
    public async Task ShouldUpdateCategory()
    {
        var request = RequestDtoSamples.UpdateCategory;
        var record = request.ToUpdateRecord();
        var expectedResponse = record.ToResponse();

        categoryRepository
            .Setup(x => x.UpdateCategory(It.Is<CategoryRecord>(y => y.id == request.Id &&
                y.name == request.Name && y.description == request.Description))).ReturnsAsync(record);

        var response = await categoryService.UpdateCategory(request);
        response.ShouldBeEquivalentTo(expectedResponse);
    }

    [Test]
    public async Task ShouldDeleteCategory()
    {
        var request = Guid.NewGuid();
        var record = RecordSamples.Category;
        var expectedResponse = record.ToResponse();

        categoryRepository
            .Setup(x => x.DeleteCategory(request)).ReturnsAsync(record);

        var response = await categoryService.DeleteCategory(request);
        response.ShouldBeEquivalentTo(expectedResponse);
    }
}