namespace BonnieYork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addHeadShotToAllTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StaffDetails", "HeadShot", c => c.String());
            AddColumn("dbo.CustomerDetails", "HeadShot", c => c.String());
            DropColumn("dbo.StaffDetails", "PicturePath");
        }
        
        public override void Down()
        {
            AddColumn("dbo.StaffDetails", "PicturePath", c => c.String());
            DropColumn("dbo.CustomerDetails", "HeadShot");
            DropColumn("dbo.StaffDetails", "HeadShot");
        }
    }
}
