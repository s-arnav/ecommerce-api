using System.Data;
using Ecommerce.Services.Constants;
using Ecommerce.Services.Records;
using Ecommerce.Services.Utilities;

namespace Ecommerce.Services.Repositories;

public interface IProductRepository
{
    Task<IEnumerable<ProductRecord>> GetAllProducts(IDbConnection connection);
    Task<ProductRecord> GetProductById(Guid id, IDbConnection connection);
    Task<ProductRecord> CreateProduct(ProductRecord productRecord, IDbConnection connection);
    Task<ProductRecord> UpdateProduct(ProductRecord productRecord, IDbConnection connection);
    Task<ProductRecord> DeleteProduct(Guid id, IDbConnection connection);
    Task<IEnumerable<ProductRecord>> GetProductsByCategory(Guid categoryId, IDbConnection connection);
}

public class ProductRepository(ISqlBuilder sqlBuilder, IDbQueryService dbQueryService)
    : BaseRepository(sqlBuilder, dbQueryService), IProductRepository
{
    protected override string SchemaName => Schemas.Ops;
    protected override string TableName => OpsTables.Product;
    
    private string GetProductsByCategorySql => $"SELECT * FROM {SchemaName}.{TableName} p WHERE p.category_id = @categoryId";

    public async Task<IEnumerable<ProductRecord>> GetAllProducts(IDbConnection connection) => await GetAll<ProductRecord>(connection);

    public async Task<ProductRecord> GetProductById(Guid id, IDbConnection connection) => await GetById<ProductRecord>(id, connection);

    public async Task<ProductRecord> CreateProduct(ProductRecord productRecord, IDbConnection connection) => await Insert(productRecord, connection);

    public async Task<ProductRecord> UpdateProduct(ProductRecord productRecord, IDbConnection connection) => await Update(productRecord, connection);
    
    public async Task<ProductRecord> DeleteProduct(Guid id, IDbConnection connection) => await Delete<ProductRecord>(id, connection);

    public async Task<IEnumerable<ProductRecord>> GetProductsByCategory(Guid categoryId, IDbConnection connection)
        => await QueryAsync<ProductRecord>(GetProductsByCategorySql, new { categoryId }, connection);
}