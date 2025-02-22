using Ecommerce.DatabaseMigrations.Constants;
using Ecommerce.Services.Records;
using Ecommerce.Services.Repositories;
using Ecommerce.Services.Utilities;
using Ecommerce.Services.Utilities.Providers;
using Ecommerce.Tests.Repositories.Fixtures;
using Ecommerce.Tests.TestExtensions;
using Shouldly;
using Xunit;

namespace Ecommerce.Tests.Repositories;

[Collection("Database collection")]
public class CategoryRepositoryTest
{
    private readonly ICategoryRepository categoryRepository;
    private readonly IDbConnectionProvider dbConnectionProvider;
    private readonly IDbQueryService dbQueryService;

    public CategoryRepositoryTest(DatabaseFixture databaseFixture)
    {
        var sqlBuilder = new SqlBuilder();
        dbConnectionProvider = new TestDbConnectionProvider(databaseFixture.ConnectionString);
        dbQueryService = new DbQueryService();
        
        categoryRepository = new CategoryRepository(sqlBuilder, dbQueryService);
    }

    [Fact]
    public async Task ShouldGetAllCategories()
    {
        var connection = dbConnectionProvider.CreateConnection();
        
        var records = await categoryRepository.GetAllCategories(connection);

        records.ShouldNotBeEmpty();
    }
    
    [Fact]
    public async Task ShouldGetCategoryById()
    {
        var id = Guid.Parse("76890632-dab2-4713-875c-be97139b7b33");
        var connection = dbConnectionProvider.CreateConnection();
        
        var record = await categoryRepository.GetCategoryById(id, connection);
        
        record.CompareRecord(SeedLists.Categories.First());
    }
    
    [Fact]
    public async Task ShouldCreateCategory()
    {
        var categoryRecord = new CategoryRecord { name = "testCategory", description = "testDescription" };
        var connection = dbConnectionProvider.CreateConnection();
        
        var originalRecordCount = (await categoryRepository.GetAllCategories(connection)).Count();
        
        var record = await categoryRepository.CreateCategory(categoryRecord, connection);
        record.CompareRecord(categoryRecord, true);
        
        var records = await categoryRepository.GetAllCategories(connection);
        records.Count().ShouldBe(originalRecordCount + 1);
    }
}