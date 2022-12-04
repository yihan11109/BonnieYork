namespace BonnieYork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addStaffWorkItemsToStaffDetail : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StoreDetails", "StaffWorkItems", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.StoreDetails", "StaffWorkItems");
        }
    }
}
