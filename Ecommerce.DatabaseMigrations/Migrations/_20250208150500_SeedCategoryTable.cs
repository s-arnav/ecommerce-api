using Ecommerce.Services.Constants;
using Ecommerce.Services.Records;
using FluentMigrator;

namespace Ecommerce.DatabaseMigrations.Migrations;

[Migration(20250208150500)]
public class _20250208150500_SeedCategoryTable : Migration
{
    private static readonly List<CategoryRecord> categories = [
        new CategoryRecord {
            id = Guid.Parse("76890632-dab2-4713-875c-be97139b7b33"),
            name = "Stationery",
            description = "Stationery Products"
        },
        new CategoryRecord {
            id = Guid.Parse("2b2bded4-399e-4fb2-80be-49ef165ea8cd"),
            name = "Gaming",
            description = "Gaming Products"
        },
        new CategoryRecord {
            id = Guid.Parse("f9c2a227-22ed-49af-9d4b-7cdaa74a72d3"),
            name = "Books",
            description = "Books & Magazines"
        }
    ];
        
    public override void Up()
    {
        foreach (var category in categories)
        {
            Insert.IntoTable(OpsTables.Category).InSchema(Schemas.Ops).Row(category);
        }
    }

    public override void Down()
    {
        Delete.FromTable(OpsTables.Category).InSchema(Schemas.Ops).AllRows();
    }
}