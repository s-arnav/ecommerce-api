using Ecommerce.Services.Repositories;
using Ecommerce.Services.RequestDtos;
using Ecommerce.Services.ResponseDtos;
using Ecommerce.Services.Utilities.Exceptions;
using Ecommerce.Services.Utilities.Extensions.Records;
using Ecommerce.Services.Utilities.Extensions.Requests;
using Ecommerce.Services.Utilities.Extensions.Validations;
using Ecommerce.Services.Utilities.Providers;
using Ecommerce.Services.Utilities.Validations;

namespace Ecommerce.Services.Services;

public interface IProductService
{
    Task<IEnumerable<ProductResponse>> GetAllProducts();
    Task<ProductResponse> GetProductById(Guid id);
    Task<ProductResponse> CreateProduct(CreateProductRequest product);
    Task<ProductResponse> UpdateProduct(UpdateProductRequest product);
    Task<ProductResponse> DeleteProduct(Guid id);
    Task<IEnumerable<ProductResponse>> GetProductsForCategory(Guid categoryId);
}

public class ProductService(IProductRepository productRepository,
    ICategoryRepository categoryRepository,
    IDbConnectionProvider dbConnectionProvider)
    : IProductService
{
    public async Task<IEnumerable<ProductResponse>> GetAllProducts()
    {
        var connection = dbConnectionProvider.CreateConnection();
        var products = await productRepository.GetAllProducts(connection);

        return products.ToResponse();
    }
    
    public async Task<ProductResponse> GetProductById(Guid id)
    {
        Validation.Begin().IsValidId(id, nameof(id));
        
        var connection = dbConnectionProvider.CreateConnection();
        var product = await productRepository.GetProductById(id, connection);
        
        return product.ToResponse();
    }

    public async Task<ProductResponse> CreateProduct(CreateProductRequest product)
    {
        product.Validate();
        
        var connection = dbConnectionProvider.CreateConnection();
        var createdProduct = await productRepository.CreateProduct(product.ToCreateRecord(), connection);
        
        return createdProduct.ToResponse();
    }

    public async Task<ProductResponse> UpdateProduct(UpdateProductRequest product)
    {
        product.Validate();
        
        var connection = dbConnectionProvider.CreateConnection();
        var updatedProduct = await productRepository.UpdateProduct(product.ToUpdateRecord(), connection);
        
        return updatedProduct.ToResponse();
    }
    
    public async Task<ProductResponse> DeleteProduct(Guid id)
    {
        Validation.Begin().IsValidId(id, nameof(id)).Check();
        
        var connection = dbConnectionProvider.CreateConnection();
        var deletedProduct = await productRepository.DeleteProduct(id, connection);

        return deletedProduct.ToResponse();
    }

    public async Task<IEnumerable<ProductResponse>> GetProductsForCategory(Guid categoryId)
    {
        Validation.Begin().IsValidId(categoryId, nameof(categoryId)).Check();
        
        var connection = dbConnectionProvider.CreateConnection();

        await categoryRepository.GetCategoryById(categoryId, connection);
        
        var records = await productRepository.GetProductsByCategory(categoryId, connection);
        
        return records.ToResponse();
    }
}
