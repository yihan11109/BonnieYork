namespace BonnieYork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editStaffWorkItems : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.StaffWorkItems", "StaffName", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.StaffWorkItems", "StaffName", c => c.Int(nullable: false));
        }
    }
}
