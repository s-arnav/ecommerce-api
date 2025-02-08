using Ecommerce.Services.Constants;
using FluentMigrator;

namespace Ecommerce.DatabaseMigrations.Migrations;

[Migration(20250208143500)]
public class _20250208143500_AddOpsSchema : Migration
{
    public override void Up()
    {
        Create.Schema(Schemas.Ops);
    }

    public override void Down()
    {
        Delete.Schema(Schemas.Ops);
    }
}