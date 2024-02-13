namespace PassionProject_YejunSon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Editusers : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "FirstName", c => c.String());
            DropColumn("dbo.Users", "FristName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "FristName", c => c.String());
            DropColumn("dbo.Users", "FirstName");
        }
    }
}
