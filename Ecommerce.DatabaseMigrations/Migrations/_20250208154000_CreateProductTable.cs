using Ecommerce.Services.Constants;
using FluentMigrator;

namespace Ecommerce.DatabaseMigrations.Migrations;

[Migration(20250208154000)]
public class _20250208154000_CreateProductTable : Migration
{
    public override void Up()
    {
        Create.Table(OpsTables.Product).InSchema(Schemas.Ops)
            .WithColumn("id").AsGuid().WithDefaultValue(SystemMethods.NewGuid).NotNullable().PrimaryKey()
            .WithColumn("name").AsString(100).NotNullable()
            .WithColumn("description").AsString(500)
            .WithColumn("price").AsDecimal().NotNullable()
            .WithColumn("quantity").AsInt32().NotNullable()
            .WithColumn("category_id").AsGuid().NotNullable().ForeignKey("FK_Product_Category", Schemas.Ops, OpsTables.Category, "id")
            .WithColumn("is_active").AsBoolean().WithDefaultValue(true).NotNullable()
            .WithColumn("is_deleted").AsBoolean().WithDefaultValue(false).NotNullable()
            .WithColumn("created_on").AsDateTime().NotNullable()
            .WithColumn("updated_on").AsDateTime().NotNullable();
        
        Create.ForeignKey()
            .FromTable(OpsTables.Product).InSchema(Schemas.Ops).ForeignColumn("category_id")
            .ToTable(OpsTables.Category).InSchema(Schemas.Ops).PrimaryColumn("id");
    }

    public override void Down()
    {
        Delete.Table(OpsTables.Product).InSchema(Schemas.Ops);
    }
}