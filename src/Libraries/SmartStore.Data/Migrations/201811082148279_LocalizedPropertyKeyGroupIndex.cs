namespace SmartStore.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class LocalizedPropertyKeyGroupIndex : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.LocalizedProperty", "LocaleKeyGroup", name: "IX_LocalizedProperty_LocaleKeyGroup");
        }

        public override void Down()
        {
            DropIndex("dbo.LocalizedProperty", "IX_LocalizedProperty_LocaleKeyGroup");
        }
    }
}
