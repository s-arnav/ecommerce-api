using Ecommerce.Services.Constants;
using FluentMigrator;
using FluentMigrator.Postgres;

namespace Ecommerce.DatabaseMigrations.Migrations;

[Migration(20250208145000)]
public class _20250208145000_CreateCategoryTable : Migration
{
    public override void Up()
    {
        Create.Table(OpsTables.Category).InSchema(Schemas.Ops)
            .WithColumn("id").AsGuid().WithDefaultValue(SystemMethods.NewGuid).NotNullable().PrimaryKey()
            .WithColumn("name").AsString(100).NotNullable()
            .WithColumn("description").AsString(500)
            .WithColumn("is_active").AsBoolean().WithDefaultValue(true).NotNullable()
            .WithColumn("is_deleted").AsBoolean().WithDefaultValue(false).NotNullable()
            .WithColumn("created_on").AsDateTime().WithDefaultValue(SystemMethods.CurrentUTCDateTime).NotNullable()
            .WithColumn("updated_on").AsDateTime().WithDefaultValue(SystemMethods.CurrentUTCDateTime).NotNullable();
    }

    public override void Down()
    {
        Delete.Table(OpsTables.Category).InSchema(Schemas.Ops);
    }
}