namespace BonnieYork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addColumnToCustomerReserve : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CustomerReserves", "ManualName", c => c.String());
            AddColumn("dbo.CustomerReserves", "ManualCellphoneNumber", c => c.String());
            AddColumn("dbo.CustomerReserves", "ManualEmail", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CustomerReserves", "ManualEmail");
            DropColumn("dbo.CustomerReserves", "ManualCellphoneNumber");
            DropColumn("dbo.CustomerReserves", "ManualName");
        }
    }
}
