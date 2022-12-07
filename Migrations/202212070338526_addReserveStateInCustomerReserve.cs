namespace BonnieYork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addReserveStateInCustomerReserve : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CustomerReserves", "ReserveState", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CustomerReserves", "ReserveState");
        }
    }
}
