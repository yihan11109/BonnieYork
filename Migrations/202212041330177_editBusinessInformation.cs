namespace BonnieYork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editBusinessInformation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BusinessInformations", "WeekdayBreakStart", c => c.String(maxLength: 50));
            AddColumn("dbo.BusinessInformations", "WeekdayBreakEnd", c => c.String(maxLength: 50));
            AddColumn("dbo.BusinessInformations", "HolidayBreakStart", c => c.String(maxLength: 50));
            AddColumn("dbo.BusinessInformations", "HolidayBreakEnd", c => c.String(maxLength: 50));
            DropColumn("dbo.BusinessInformations", "WeekdayBreakTime");
            DropColumn("dbo.BusinessInformations", "HolidayBreakTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.BusinessInformations", "HolidayBreakTime", c => c.String(maxLength: 50));
            AddColumn("dbo.BusinessInformations", "WeekdayBreakTime", c => c.String(maxLength: 50));
            DropColumn("dbo.BusinessInformations", "HolidayBreakEnd");
            DropColumn("dbo.BusinessInformations", "HolidayBreakStart");
            DropColumn("dbo.BusinessInformations", "WeekdayBreakEnd");
            DropColumn("dbo.BusinessInformations", "WeekdayBreakStart");
        }
    }
}
