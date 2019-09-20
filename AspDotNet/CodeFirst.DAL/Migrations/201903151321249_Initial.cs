namespace CodeFirst.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EntityVehicles",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Capacity = c.Int(nullable: false),
                        Color = c.String(nullable: false),
                        Model = c.String(nullable: false),
                        Power = c.Int(nullable: false),
                        SeatNumber = c.Int(nullable: false),
                        WeelsNumber = c.Int(nullable: false),
                        CustomPainting = c.Int(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        Garage_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.EntityGarages", t => t.Garage_ID)
                .Index(t => t.Garage_ID);
            
            CreateTable(
                "dbo.EntityGarages",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EntityVehicles", "Garage_ID", "dbo.EntityGarages");
            DropIndex("dbo.EntityVehicles", new[] { "Garage_ID" });
            DropTable("dbo.EntityGarages");
            DropTable("dbo.EntityVehicles");
        }
    }
}
