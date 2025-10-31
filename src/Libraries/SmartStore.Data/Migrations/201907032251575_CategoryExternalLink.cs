namespace SmartStore.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class CategoryExternalLink : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Category", "ExternalLink", c => c.String(maxLength: 255));
        }

        public override void Down()
        {
            DropColumn("dbo.Category", "ExternalLink");
        }
    }
}
