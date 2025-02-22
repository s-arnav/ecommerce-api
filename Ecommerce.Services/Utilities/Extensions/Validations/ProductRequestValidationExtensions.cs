using Ecommerce.Services.RequestDtos;
using Ecommerce.Services.Utilities.Validations;

namespace Ecommerce.Services.Utilities.Extensions.Validations;

public static class ProductRequestValidationExtensions
{
    public static Validation Validate(this CreateProductRequest request)
        => Validation.Begin()
            .IsNotNullOrEmptyString(request.Name, nameof(request.Name))
            .IsNotNullOrEmptyString(request.Description, nameof(request.Description))
            .IsMinValue(request.Quantity, 1, nameof(request.Quantity))
            .IsValidId(request.CategoryId, nameof(request.CategoryId))
            .IsMinValue(request.Price, 1, nameof(request.Price))
            .Check();
    
    public static Validation Validate(this UpdateProductRequest request)
        => Validation.Begin()
            .IsValidId(request.Id, nameof(request.Id)).Check()
            .IsNotNullOrEmptyString(request.Name, nameof(request.Name))
            .IsNotNullOrEmptyString(request.Description, nameof(request.Description))
            .IsMinValue(request.Quantity, 1, nameof(request.Quantity))
            .IsValidId(request.CategoryId, nameof(request.CategoryId))
            .IsMinValue(request.Price, 1, nameof(request.Price))
            .Check();
}