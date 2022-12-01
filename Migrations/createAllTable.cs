namespace BonnieYork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createAllTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CustomerDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Account = c.String(nullable: false, maxLength: 100),
                        Password = c.String(nullable: false, maxLength: 100),
                        CustomerName = c.String(nullable: false, maxLength: 50),
                        CellphoneNumber = c.String(nullable: false),
                        BirthDay = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Industries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IndustryName = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.StoreDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IndustryId = c.Int(),
                        Account = c.String(nullable: false, maxLength: 100),
                        Password = c.String(nullable: false, maxLength: 100),
                        StoreName = c.String(nullable: false, maxLength: 50),
                        City = c.String(nullable: false, maxLength: 50),
                        District = c.String(nullable: false, maxLength: 50),
                        Address = c.String(nullable: false, maxLength: 50),
                        CellphoneNumber = c.String(nullable: false, maxLength: 15),
                        PhoneNumber = c.String(maxLength: 15),
                        StaffTitle = c.String(maxLength: 20),
                        Description = c.String(),
                        BannerPath = c.String(),
                        FacebookLink = c.String(),
                        InstagramLink = c.String(),
                        LineLink = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Industries", t => t.IndustryId)
                .Index(t => t.IndustryId);
            
            CreateTable(
                "dbo.StaffDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StoreId = c.Int(nullable: false),
                        Account = c.String(nullable: false, maxLength: 100),
                        Password = c.String(nullable: false, maxLength: 100),
                        StaffName = c.String(nullable: false, maxLength: 20),
                        JobTitle = c.String(maxLength: 20),
                        PicturePath = c.String(),
                        CellphoneNumber = c.String(maxLength: 20),
                        Introduction = c.String(),
                        FacebookLink = c.String(),
                        InstagramLink = c.String(),
                        LineLink = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.StoreDetails", t => t.StoreId, cascadeDelete: true)
                .Index(t => t.StoreId);
            
            CreateTable(
                "dbo.StaffHolidays",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StaffId = c.Int(),
                        StaffDaysOff = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.StaffDetails", t => t.StaffId)
                .Index(t => t.StaffId);

            CreateTable(
                    "dbo.BusinessInformations",
                    c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StoreId = c.Int(),
                        TimeInterval = c.String(maxLength: 50),
                        WeekdayStartTime = c.String(maxLength: 50),
                        WeekdayEndTime = c.String(maxLength: 50),
                        WeekdayBreakTime = c.String(maxLength: 50),
                        HolidayStartTime = c.String(maxLength: 50),
                        HolidayEndTime = c.String(maxLength: 50),
                        HolidayBreakTime = c.String(maxLength: 50),
                        PublicHoliday = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.StoreDetails", t => t.StoreId)
                .Index(t => t.StoreId);

        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StaffDetails", "StoreId", "dbo.StoreDetails");
            DropForeignKey("dbo.StaffHolidays", "StaffId", "dbo.StaffDetails");
            DropForeignKey("dbo.StoreDetails", "IndustryId", "dbo.Industries");
            DropIndex("dbo.StaffHolidays", new[] { "StaffId" });
            DropIndex("dbo.StaffDetails", new[] { "StoreId" });
            DropIndex("dbo.StoreDetails", new[] { "IndustryId" });
            DropTable("dbo.StaffHolidays");
            DropTable("dbo.StaffDetails");
            DropTable("dbo.StoreDetails");
            DropTable("dbo.Industries");
            DropTable("dbo.CustomerDetails");
        }
    }
}
