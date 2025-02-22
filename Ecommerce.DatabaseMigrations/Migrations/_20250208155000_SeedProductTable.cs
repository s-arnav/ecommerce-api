using Ecommerce.DatabaseMigrations.Constants;
using Ecommerce.Services.Constants;
using Ecommerce.Services.Records;
using FluentMigrator;

namespace Ecommerce.DatabaseMigrations.Migrations;

[Migration(20250208155000)]
public class _20250208155000_SeedProductTable : Migration
{
    public override void Up()
    {
        foreach (var product in SeedLists.Products)
        {
            Insert.IntoTable(OpsTables.Product).InSchema(Schemas.Ops).Row(product);
        }
    }

    public override void Down()
    {
        Delete.FromTable(OpsTables.Product).InSchema(Schemas.Ops).AllRows();
    }
}