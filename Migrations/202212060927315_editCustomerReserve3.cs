namespace BonnieYork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editCustomerReserve3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CustomerReserves", "StaffName", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CustomerReserves", "StaffName", c => c.Int(nullable: false));
        }
    }
}
