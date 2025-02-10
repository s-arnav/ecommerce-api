using Ecommerce.Services.Constants;
using Ecommerce.Services.Records;
using FluentMigrator;

namespace Ecommerce.DatabaseMigrations.Migrations;

[Migration(20250208155000)]
public class _20250208155000_SeedProductTable : Migration
{
    private static readonly ProductRecord product = new()
        {
            id = Guid.NewGuid(),
            name = "Sheaffer Taranis",
            description = "Sheaffer's 100th Anniversary Launch",
            quantity = 10,
            price = 25000m,
            category_id = Guid.Parse("76890632-dab2-4713-875c-be97139b7b33")
        };
    
    public override void Up()
    {
        Insert.IntoTable(OpsTables.Product).InSchema(Schemas.Ops).Row(product);
    }

    public override void Down()
    {
        Delete.FromTable(OpsTables.Product).InSchema(Schemas.Ops).AllRows();
    }
}