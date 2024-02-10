namespace PassionProject_YejunSon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RestaurantsFolders : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RestaurantsFolders",
                c => new
                    {
                        RestaurantsFolderId = c.Int(nullable: false, identity: true),
                        FolderName = c.String(),
                    })
                .PrimaryKey(t => t.RestaurantsFolderId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.RestaurantsFolders");
        }
    }
}
