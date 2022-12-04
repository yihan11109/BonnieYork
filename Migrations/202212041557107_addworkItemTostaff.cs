namespace BonnieYork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addworkItemTostaff : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StaffDetails", "StaffWorkItems", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.StaffDetails", "StaffWorkItems");
        }
    }
}
