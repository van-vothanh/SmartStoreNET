namespace SmartStore.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class ExportAttributeMappings : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductAttribute", "ExportMappings", c => c.String());
        }

        public override void Down()
        {
            DropColumn("dbo.ProductAttribute", "ExportMappings");
        }
    }
}
