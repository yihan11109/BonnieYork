namespace BonnieYork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStaffWorkItems : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StaffWorkItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BusinessItemsId = c.Int(nullable: false),
                        StaffId = c.Int(nullable: false),
                        StaffName = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BusinessItems", t => t.BusinessItemsId, cascadeDelete: true)
                .Index(t => t.BusinessItemsId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StaffWorkItems", "BusinessItemsId", "dbo.BusinessItems");
            DropIndex("dbo.StaffWorkItems", new[] { "BusinessItemsId" });
            DropTable("dbo.StaffWorkItems");
        }
    }
}
