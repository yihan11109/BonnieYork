namespace BonnieYork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editCustomerReserve : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CustomerReserves", "StoreId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CustomerReserves", "StoreId");
        }
    }
}
