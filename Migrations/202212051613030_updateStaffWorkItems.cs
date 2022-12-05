namespace BonnieYork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateStaffWorkItems : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.StaffDetails", "StaffWorkItems");
        }
        
        public override void Down()
        {
            AddColumn("dbo.StaffDetails", "StaffWorkItems", c => c.String());
        }
    }
}
