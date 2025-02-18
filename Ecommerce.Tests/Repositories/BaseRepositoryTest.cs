using System.Data;
using Ecommerce.Services.Repositories;
using Ecommerce.Services.Utilities;
using Ecommerce.Services.Utilities.Providers;
using Moq;
using Xunit;

namespace Ecommerce.Tests.Repositories;

public class BaseRepositoryTest
{
    private readonly MockRepository mockRepository;
    private readonly Mock<ISqlBuilder> sqlBuilder;
    private readonly Mock<IDbConnectionProvider> dbConnectionProvider;
    private readonly Mock<IDbConnection> dbConnection;
    
    private readonly BaseRepositoryConcrete baseRepository;

    public BaseRepositoryTest()
    {
        mockRepository = new MockRepository(MockBehavior.Strict);
        sqlBuilder = mockRepository.Create<ISqlBuilder>();
        dbConnectionProvider = mockRepository.Create<IDbConnectionProvider>();
        dbConnection = mockRepository.Create<IDbConnection>();
        baseRepository = new BaseRepositoryConcrete(sqlBuilder.Object, dbConnectionProvider.Object);
    }
    
    [Fact]
    public void ShouldMergeRecords()
    {
    }

    private class BaseRepositoryConcrete(ISqlBuilder sqlBuilder, IDbConnectionProvider connectionProvider)
        : BaseRepository(sqlBuilder, connectionProvider)
    {
    }
}