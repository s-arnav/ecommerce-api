using FluentMigrator;

namespace Ecommerce.DatabaseMigrations.Migrations;

[Migration(20250208135000)]
public class _20250208135000_AddUuidExtension : Migration
{
    public override void Up()
    {
        Execute.Sql("CREATE EXTENSION IF NOT EXISTS \"uuid-ossp\";");
    }

    public override void Down()
    {
        Execute.Sql("DROP EXTENSION IF EXISTS \"uuid-ossp\";");
    }
}