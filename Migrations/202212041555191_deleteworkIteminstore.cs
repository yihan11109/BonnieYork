namespace BonnieYork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deleteworkIteminstore : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.StoreDetails", "StaffWorkItems");
        }
        
        public override void Down()
        {
            AddColumn("dbo.StoreDetails", "StaffWorkItems", c => c.String());
        }
    }
}
