using System.Data;
using Ecommerce.Services.Records;
using Ecommerce.Services.Repositories;
using Ecommerce.Services.Services;
using Ecommerce.Services.Utilities.Extensions.Records;
using Ecommerce.Services.Utilities.Extensions.Requests;
using Ecommerce.Services.Utilities.Providers;
using Ecommerce.Tests.Utilities.Samples;
using Moq;
using Shouldly;
using Xunit;

namespace Ecommerce.Tests.Services;

public class CategoryServiceTest : IDisposable
{
    private readonly CategoryService categoryService;
    private readonly Mock<ICategoryRepository> categoryRepository;
    private readonly Mock<IDbConnectionProvider> dbConnectionProvider;

    public CategoryServiceTest()
    {
        categoryRepository = new Mock<ICategoryRepository>();
        dbConnectionProvider = new Mock<IDbConnectionProvider>();
        categoryService = new CategoryService(categoryRepository.Object, dbConnectionProvider.Object);
    }

    public void Dispose()
    {
        categoryRepository.VerifyAll();
        dbConnectionProvider.VerifyAll();
    }

    [Fact]
    public async Task ShouldGetAllCategories()
    {
        var records = RecordSamples.Categories();
        var expectedResponse = records.Select(x => x.ToResponse());
        var connection = new Mock<IDbConnection>();

        dbConnectionProvider.Setup(x => x.CreateConnection()).Returns(connection.Object);
        categoryRepository.Setup(x => x.GetAllCategories(connection.Object)).ReturnsAsync(records);

        var response = await categoryService.GetAllCategories();
        response.ShouldBeEquivalentTo(expectedResponse);
    }

    [Fact]
    public async Task ShouldGetCategory()
    {
        var id = Guid.NewGuid();
        var record = RecordSamples.Category;
        record.id = id;
        var expectedResponse = record.ToResponse();
        var connection = new Mock<IDbConnection>();

        dbConnectionProvider.Setup(x => x.CreateConnection()).Returns(connection.Object);
        categoryRepository.Setup(x => x.GetCategoryById(id, connection.Object)).ReturnsAsync(record);

        var response = await categoryService.GetCategoryById(id);
        response.ShouldBeEquivalentTo(expectedResponse);
    }

    [Fact]
    public async Task ShouldCreateCategory()
    {
        var request = RequestDtoSamples.CreateCategory;
        var record = new CategoryRecord { id = Guid.NewGuid(), name = request.Name, description = request.Description };
        var expectedResponse = record.ToResponse();
        var connection = new Mock<IDbConnection>();

        dbConnectionProvider.Setup(x => x.CreateConnection()).Returns(connection.Object);
        categoryRepository
            .Setup(x => x.CreateCategory(It.Is<CategoryRecord>(y => y.name == request.Name && y.description == request.Description), connection.Object))
            .ReturnsAsync(record);

        var response = await categoryService.CreateCategory(request);
        response.ShouldBeEquivalentTo(expectedResponse);
    }

    [Fact]
    public async Task ShouldUpdateCategory()
    {
        var request = RequestDtoSamples.UpdateCategory;
        var record = request.ToUpdateRecord();
        var expectedResponse = record.ToResponse();
        var connection = new Mock<IDbConnection>();

        dbConnectionProvider.Setup(x => x.CreateConnection()).Returns(connection.Object);
        categoryRepository.Setup(x => x.UpdateCategory(
                It.Is<CategoryRecord>(y => y.id == request.Id && y.name == request.Name && y.description == request.Description), connection.Object))
            .ReturnsAsync(record);

        var response = await categoryService.UpdateCategory(request);
        response.ShouldBeEquivalentTo(expectedResponse);
    }

    [Fact]
    public async Task ShouldDeleteCategory()
    {
        var request = Guid.NewGuid();
        var record = RecordSamples.Category;
        var expectedResponse = record.ToResponse();
        var connection = new Mock<IDbConnection>();

        dbConnectionProvider.Setup(x => x.CreateConnection()).Returns(connection.Object);
        categoryRepository.Setup(x => x.DeleteCategory(request, connection.Object)).ReturnsAsync(record);

        var response = await categoryService.DeleteCategory(request);
        response.ShouldBeEquivalentTo(expectedResponse);
    }
}