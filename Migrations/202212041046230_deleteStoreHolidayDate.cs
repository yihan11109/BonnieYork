namespace BonnieYork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deleteStoreHolidayDate : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.StoreHolidayDates", "StoreId", "dbo.StoreDetails");
            DropIndex("dbo.StoreHolidayDates", new[] { "StoreId" });
            DropTable("dbo.StoreHolidayDates");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.StoreHolidayDates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StoreId = c.Int(nullable: false),
                        HolidayDate = c.String(nullable: false, maxLength: 10),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.StoreHolidayDates", "StoreId");
            AddForeignKey("dbo.StoreHolidayDates", "StoreId", "dbo.StoreDetails", "Id", cascadeDelete: true);
        }
    }
}
