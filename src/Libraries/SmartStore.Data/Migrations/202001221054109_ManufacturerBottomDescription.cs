namespace SmartStore.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class ManufacturerBottomDescription : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Manufacturer", "BottomDescription", c => c.String());
            CreateIndex("dbo.PermissionRecord", "SystemName");
        }

        public override void Down()
        {
            DropIndex("dbo.PermissionRecord", new[] { "SystemName" });
            DropColumn("dbo.Manufacturer", "BottomDescription");
        }
    }
}
