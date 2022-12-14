namespace BonnieYork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addItemPriceToReserve : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CustomerReserves", "Price", c => c.String(maxLength: 10));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CustomerReserves", "Price");
        }
    }
}
