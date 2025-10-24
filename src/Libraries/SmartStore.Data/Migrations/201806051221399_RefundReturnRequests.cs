namespace SmartStore.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class RefundReturnRequests : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ReturnRequest", "RefundToWallet", c => c.Boolean());
        }

        public override void Down()
        {
            DropColumn("dbo.ReturnRequest", "RefundToWallet");
        }
    }
}
