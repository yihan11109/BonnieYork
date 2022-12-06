namespace BonnieYork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editCustomerReserve5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CustomerReserves", "ReserveEnd", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CustomerReserves", "ReserveEnd");
        }
    }
}
