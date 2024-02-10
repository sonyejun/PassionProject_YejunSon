namespace PassionProject_YejunSon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Restaurants : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Restaurants",
                c => new
                    {
                        RestaurantId = c.Int(nullable: false, identity: true),
                        RestaurantName = c.String(),
                        Location = c.String(),
                        Rate = c.Int(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.RestaurantId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Restaurants");
        }
    }
}
