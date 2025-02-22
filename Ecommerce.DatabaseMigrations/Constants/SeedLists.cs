using Ecommerce.Services.Records;

namespace Ecommerce.DatabaseMigrations.Constants;

public static class SeedLists
{
    public static readonly List<CategoryRecord> Categories =
    [
        new()
        {
            id = Guid.Parse("76890632-dab2-4713-875c-be97139b7b33"),
            name = "Stationery",
            description = "Stationery Products"
        },
        new()
        {
            id = Guid.Parse("2b2bded4-399e-4fb2-80be-49ef165ea8cd"),
            name = "Gaming",
            description = "Gaming Products"
        },
        new()
        {
            id = Guid.Parse("f9c2a227-22ed-49af-9d4b-7cdaa74a72d3"),
            name = "Books",
            description = "Books & Magazines"
        }
    ];

    public static readonly List<ProductRecord> Products =
    [
        new()
        {
            id = Guid.NewGuid(),
            name = "Sheaffer Taranis",
            description = "Sheaffer's 100th Anniversary Launch",
            quantity = 10,
            price = 25000m,
            category_id = Guid.Parse("76890632-dab2-4713-875c-be97139b7b33")
        },
        new()
        {
            id = Guid.NewGuid(),
            name = "PS5 Dualsense Controller",
            description = "Dualsense Controller for PS5 with haptic feedback and adaptive triggers",
            quantity = 10,
            price = 6000m,
            category_id = Guid.Parse("2b2bded4-399e-4fb2-80be-49ef165ea8cd")
        }
    ];
}