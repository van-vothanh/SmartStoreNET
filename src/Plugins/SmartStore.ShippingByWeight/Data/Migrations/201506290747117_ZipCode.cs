namespace SmartStore.ShippingByWeight.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class ZipCode : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ShippingByWeight", "Zip", c => c.String());
        }

        public override void Down()
        {
            DropColumn("dbo.ShippingByWeight", "Zip");
        }
    }
}
