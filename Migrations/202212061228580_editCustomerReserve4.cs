namespace BonnieYork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editCustomerReserve4 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.CustomerReserves", "ReserveEnd");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CustomerReserves", "ReserveEnd", c => c.DateTime(nullable: false));
        }
    }
}
