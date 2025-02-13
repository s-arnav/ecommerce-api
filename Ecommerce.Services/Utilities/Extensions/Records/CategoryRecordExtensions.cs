using Ecommerce.Services.Records;
using Ecommerce.Services.ResponseDtos;

namespace Ecommerce.Services.Utilities.Extensions.Records;

public static class CategoryRecordExtensions
{
    public static CategoryResponse ToResponse(this CategoryRecord categoryRecord)
        => new()
        {
            Id = categoryRecord.id,
            Name = categoryRecord.name,
            Description = categoryRecord.description,
            IsActive = categoryRecord.is_active
        };
}