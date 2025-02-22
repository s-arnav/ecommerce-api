using Ecommerce.DatabaseMigrations.Constants;
using Ecommerce.Services.Constants;
using Ecommerce.Services.Records;
using FluentMigrator;

namespace Ecommerce.DatabaseMigrations.Migrations;

[Migration(20250208150500)]
public class _20250208150500_SeedCategoryTable : Migration
{
    public override void Up()
    {
        foreach (var category in SeedLists.Categories)
        {
            Insert.IntoTable(OpsTables.Category).InSchema(Schemas.Ops).Row(category);
        }
    }

    public override void Down()
    {
        Delete.FromTable(OpsTables.Category).InSchema(Schemas.Ops).AllRows();
    }
}