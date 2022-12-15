namespace BonnieYork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addMyFavourite : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MyFavourites",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerId = c.Int(nullable: false),
                        StoreId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CustomerDetails", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.StoreDetails", t => t.StoreId, cascadeDelete: true)
                .Index(t => t.CustomerId)
                .Index(t => t.StoreId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MyFavourites", "StoreId", "dbo.StoreDetails");
            DropForeignKey("dbo.MyFavourites", "CustomerId", "dbo.CustomerDetails");
            DropIndex("dbo.MyFavourites", new[] { "StoreId" });
            DropIndex("dbo.MyFavourites", new[] { "CustomerId" });
            DropTable("dbo.MyFavourites");
        }
    }
}
