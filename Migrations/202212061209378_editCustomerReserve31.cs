namespace BonnieYork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editCustomerReserve31 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CustomerReserves", "CustomerId", "dbo.CustomerDetails");
            DropIndex("dbo.CustomerReserves", new[] { "CustomerId" });
            AlterColumn("dbo.CustomerReserves", "CustomerId", c => c.Int());
            CreateIndex("dbo.CustomerReserves", "CustomerId");
            AddForeignKey("dbo.CustomerReserves", "CustomerId", "dbo.CustomerDetails", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CustomerReserves", "CustomerId", "dbo.CustomerDetails");
            DropIndex("dbo.CustomerReserves", new[] { "CustomerId" });
            AlterColumn("dbo.CustomerReserves", "CustomerId", c => c.Int(nullable: false));
            CreateIndex("dbo.CustomerReserves", "CustomerId");
            AddForeignKey("dbo.CustomerReserves", "CustomerId", "dbo.CustomerDetails", "Id", cascadeDelete: true);
        }
    }
}
