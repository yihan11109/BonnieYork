namespace BonnieYork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddVirtualStoreDetail : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StoreDetails", "BusinessInformationId", c => c.Int());
            CreateIndex("dbo.StoreDetails", "BusinessInformationId");
            AddForeignKey("dbo.StoreDetails", "BusinessInformationId", "dbo.BusinessInformations", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StoreDetails", "BusinessInformationId", "dbo.BusinessInformations");
            DropIndex("dbo.StoreDetails", new[] { "BusinessInformationId" });
            DropColumn("dbo.StoreDetails", "BusinessInformationId");
        }
    }
}
