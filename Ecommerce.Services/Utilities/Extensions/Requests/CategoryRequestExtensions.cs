using Ecommerce.Services.Records;
using Ecommerce.Services.RequestDtos;

namespace Ecommerce.Services.Utilities.Extensions.Requests;

public static class CategoryRequestExtensions
{
    public static CategoryRecord ToCreateRecord(this CreateCategoryRequest request)
        => new()
        {
            name = request.Name,
            description = request.Description
        };
    
    public static CategoryRecord ToUpdateRecord(this UpdateCategoryRequest request)
        => new()
        {
            id = request.Id,
            name = request.Name,
            description = request.Description
        };
}