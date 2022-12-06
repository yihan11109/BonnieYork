namespace BonnieYork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editCustomerReserve7 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CustomerReserves", "ReserveStart", c => c.DateTime(nullable: false));
            AddColumn("dbo.CustomerReserves", "ReserveEnd", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CustomerReserves", "ReserveEnd");
            DropColumn("dbo.CustomerReserves", "ReserveStart");
        }
    }
}
