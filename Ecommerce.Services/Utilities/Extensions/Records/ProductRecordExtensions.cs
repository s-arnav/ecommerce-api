using Ecommerce.Services.Records;
using Ecommerce.Services.ResponseDtos;

namespace Ecommerce.Services.Utilities.Extensions.Records;

public static class ProductRecordExtensions
{
    public static ProductResponse ToResponse(this ProductRecord productRecord)
        => new()
        {
            Id = productRecord.id,
            Name = productRecord.name,
            Description = productRecord.description,
            Price = productRecord.price,
            Quantity = productRecord.quantity,
            CategoryId = productRecord.category_id,
            IsActive = productRecord.is_active
        };

    public static IEnumerable<ProductResponse> ToResponse(this IEnumerable<ProductRecord> productRecords)
        => productRecords.Select(p => p.ToResponse());
}