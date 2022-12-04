namespace BonnieYork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStoreHolidayDateTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StoreHolidayDates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StoreId = c.Int(nullable: false),
                        HolidayDate = c.String(nullable: false, maxLength: 10),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.StoreDetails", t => t.StoreId, cascadeDelete: true)
                .Index(t => t.StoreId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StoreHolidayDates", "StoreId", "dbo.StoreDetails");
            DropIndex("dbo.StoreHolidayDates", new[] { "StoreId" });
            DropTable("dbo.StoreHolidayDates");
        }
    }
}
