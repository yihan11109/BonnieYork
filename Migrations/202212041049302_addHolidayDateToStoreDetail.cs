namespace BonnieYork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addHolidayDateToStoreDetail : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StoreDetails", "HolidayDate", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.StoreDetails", "HolidayDate");
        }
    }
}
