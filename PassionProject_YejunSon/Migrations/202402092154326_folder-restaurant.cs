namespace PassionProject_YejunSon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class folderrestaurant : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Restaurants", "UserId", "dbo.Users");
            DropForeignKey("dbo.RestaurantsFolders", "UserId", "dbo.Users");
            CreateTable(
                "dbo.RestaurantsFolderRestaurants",
                c => new
                    {
                        RestaurantsFolder_RestaurantsFolderId = c.Int(nullable: false),
                        Restaurant_RestaurantId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.RestaurantsFolder_RestaurantsFolderId, t.Restaurant_RestaurantId })
                .ForeignKey("dbo.RestaurantsFolders", t => t.RestaurantsFolder_RestaurantsFolderId, cascadeDelete: true)
                .ForeignKey("dbo.Restaurants", t => t.Restaurant_RestaurantId, cascadeDelete: true)
                .Index(t => t.RestaurantsFolder_RestaurantsFolderId)
                .Index(t => t.Restaurant_RestaurantId);
            
            AddForeignKey("dbo.Restaurants", "UserId", "dbo.Users", "UserId");
            AddForeignKey("dbo.RestaurantsFolders", "UserId", "dbo.Users", "UserId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RestaurantsFolders", "UserId", "dbo.Users");
            DropForeignKey("dbo.Restaurants", "UserId", "dbo.Users");
            DropForeignKey("dbo.RestaurantsFolderRestaurants", "Restaurant_RestaurantId", "dbo.Restaurants");
            DropForeignKey("dbo.RestaurantsFolderRestaurants", "RestaurantsFolder_RestaurantsFolderId", "dbo.RestaurantsFolders");
            DropIndex("dbo.RestaurantsFolderRestaurants", new[] { "Restaurant_RestaurantId" });
            DropIndex("dbo.RestaurantsFolderRestaurants", new[] { "RestaurantsFolder_RestaurantsFolderId" });
            DropTable("dbo.RestaurantsFolderRestaurants");
            AddForeignKey("dbo.RestaurantsFolders", "UserId", "dbo.Users", "UserId", cascadeDelete: true);
            AddForeignKey("dbo.Restaurants", "UserId", "dbo.Users", "UserId", cascadeDelete: true);
        }
    }
}
