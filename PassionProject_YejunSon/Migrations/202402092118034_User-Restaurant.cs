namespace PassionProject_YejunSon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserRestaurant : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Restaurants", "UserId", c => c.Int(nullable: false));
            CreateIndex("dbo.Restaurants", "UserId");
            AddForeignKey("dbo.Restaurants", "UserId", "dbo.Users", "UserId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Restaurants", "UserId", "dbo.Users");
            DropIndex("dbo.Restaurants", new[] { "UserId" });
            DropColumn("dbo.Restaurants", "UserId");
        }
    }
}
