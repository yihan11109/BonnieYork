namespace BonnieYork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editStoreDtail : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StoreDetails", "HeadShot", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.StoreDetails", "HeadShot");
        }
    }
}
