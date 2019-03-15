namespace CodeFirst.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EntityVehicles", "Turbo", c => c.Boolean());
            AddColumn("dbo.EntityGarages", "Name", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.EntityGarages", "Name");
            DropColumn("dbo.EntityVehicles", "Turbo");
        }
    }
}
