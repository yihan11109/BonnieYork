namespace BonnieYork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editCustomerReserve2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CustomerReserves", "StaffName", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CustomerReserves", "StaffName");
        }
    }
}
