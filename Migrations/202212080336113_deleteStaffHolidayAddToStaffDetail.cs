namespace BonnieYork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deleteStaffHolidayAddToStaffDetail : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.StaffHolidays", "StaffId", "dbo.StaffDetails");
            DropIndex("dbo.StaffHolidays", new[] { "StaffId" });
            AddColumn("dbo.StaffDetails", "StaffDaysOff", c => c.String());
            DropTable("dbo.StaffHolidays");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.StaffHolidays",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StaffId = c.Int(),
                        StaffDaysOff = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropColumn("dbo.StaffDetails", "StaffDaysOff");
            CreateIndex("dbo.StaffHolidays", "StaffId");
            AddForeignKey("dbo.StaffHolidays", "StaffId", "dbo.StaffDetails", "Id");
        }
    }
}
