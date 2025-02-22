using Ecommerce.Services.Records;
using Ecommerce.Services.RequestDtos;

namespace Ecommerce.Services.Utilities.Extensions.Requests;

public static class ProductRequestExtensions
{
    public static ProductRecord ToCreateRecord(this CreateProductRequest request)
        => new()
        {
            name = request.Name,
            description = request.Description,
            price = request.Price,
            quantity = request.Quantity,
            category_id = request.CategoryId,
            is_active = request.IsActive
        };
    
    public static ProductRecord ToUpdateRecord(this UpdateProductRequest request)
        => new()
        {
            id = request.Id,
            name = request.Name,
            description = request.Description,
            price = request.Price,
            quantity = request.Quantity,
            category_id = request.CategoryId,
            is_active = request.IsActive
        };
}