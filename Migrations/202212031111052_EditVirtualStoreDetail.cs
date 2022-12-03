namespace BonnieYork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditVirtualStoreDetail : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.StoreDetails", "BusinessInformationId", "dbo.BusinessInformations");
            DropIndex("dbo.StoreDetails", new[] { "BusinessInformationId" });
            DropColumn("dbo.StoreDetails", "BusinessInformationId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.StoreDetails", "BusinessInformationId", c => c.Int());
            CreateIndex("dbo.StoreDetails", "BusinessInformationId");
            AddForeignKey("dbo.StoreDetails", "BusinessInformationId", "dbo.BusinessInformations", "Id");
        }
    }
}
