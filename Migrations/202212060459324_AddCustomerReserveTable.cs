namespace BonnieYork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCustomerReserveTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CustomerReserves",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerId = c.Int(nullable: false),
                        StaffId = c.Int(nullable: false),
                        ItemId = c.Int(nullable: false),
                        ReserveDate = c.DateTime(nullable: false),
                        ReserveStart = c.DateTime(nullable: false),
                        ReserveEnd = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BusinessItems", t => t.ItemId, cascadeDelete: true)
                .ForeignKey("dbo.CustomerDetails", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId)
                .Index(t => t.ItemId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CustomerReserves", "CustomerId", "dbo.CustomerDetails");
            DropForeignKey("dbo.CustomerReserves", "ItemId", "dbo.BusinessItems");
            DropIndex("dbo.CustomerReserves", new[] { "ItemId" });
            DropIndex("dbo.CustomerReserves", new[] { "CustomerId" });
            DropTable("dbo.CustomerReserves");
        }
    }
}
