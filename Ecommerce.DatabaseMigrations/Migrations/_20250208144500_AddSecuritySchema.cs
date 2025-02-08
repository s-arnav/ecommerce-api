using Ecommerce.Services.Constants;
using FluentMigrator;

namespace Ecommerce.DatabaseMigrations.Migrations;

[Migration(20250208144500)]
public class _20250208144500_AddSecuritySchema : Migration
{
    public override void Up()
    {
        Create.Schema(Schemas.Security);
    }

    public override void Down()
    {
        Delete.Schema(Schemas.Security);
    }
}