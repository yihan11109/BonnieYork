namespace BonnieYork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editCustomerReserve6 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.CustomerReserves", "ReserveStart");
            DropColumn("dbo.CustomerReserves", "ReserveEnd");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CustomerReserves", "ReserveEnd", c => c.DateTime(nullable: false));
            AddColumn("dbo.CustomerReserves", "ReserveStart", c => c.DateTime(nullable: false));
        }
    }
}
