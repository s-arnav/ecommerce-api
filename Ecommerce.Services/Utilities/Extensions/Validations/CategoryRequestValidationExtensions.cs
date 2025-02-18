using Ecommerce.Services.RequestDtos;
using Ecommerce.Services.Utilities.Validations;

namespace Ecommerce.Services.Utilities.Extensions.Validations;

public static class CategoryRequestValidationExtensions
{
    public static void Validate(this CreateCategoryRequest category)
        => Validation.Begin()
            .IsNotNullOrEmptyString(category.Name, nameof(category.Name))
            .IsNotNullOrEmptyString(category.Description, nameof(category.Description))
            .Check();
    
    public static void Validate(this UpdateCategoryRequest category)
        => Validation.Begin()
            .IsValidId(category.Id, nameof(category.Id))
            .Check()
            .IsNotNullOrEmptyString(category.Name, nameof(category.Name))
            .IsNotNullOrEmptyString(category.Description, nameof(category.Description))
            .Check();
}