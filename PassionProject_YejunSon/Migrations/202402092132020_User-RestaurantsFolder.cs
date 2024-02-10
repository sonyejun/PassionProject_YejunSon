namespace PassionProject_YejunSon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserRestaurantsFolder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RestaurantsFolders", "UserId", c => c.Int(nullable: false));
            CreateIndex("dbo.RestaurantsFolders", "UserId");
            AddForeignKey("dbo.RestaurantsFolders", "UserId", "dbo.Users", "UserId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RestaurantsFolders", "UserId", "dbo.Users");
            DropIndex("dbo.RestaurantsFolders", new[] { "UserId" });
            DropColumn("dbo.RestaurantsFolders", "UserId");
        }
    }
}
