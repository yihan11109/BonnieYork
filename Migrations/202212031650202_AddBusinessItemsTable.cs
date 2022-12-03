namespace BonnieYork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBusinessItemsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BusinessItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StoreId = c.Int(nullable: false),
                        ItemName = c.String(nullable: false, maxLength: 10),
                        SpendTime = c.String(nullable: false, maxLength: 10),
                        Price = c.String(nullable: false, maxLength: 10),
                        Describe = c.String(),
                        Remark = c.String(),
                        PicturePath = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.StoreDetails", t => t.StoreId, cascadeDelete: true)
                .Index(t => t.StoreId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BusinessItems", "StoreId", "dbo.StoreDetails");
            DropIndex("dbo.BusinessItems", new[] { "StoreId" });
            DropTable("dbo.BusinessItems");
        }
    }
}
