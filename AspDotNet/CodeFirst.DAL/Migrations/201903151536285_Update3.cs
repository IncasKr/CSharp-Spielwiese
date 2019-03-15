namespace CodeFirst.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EntityVehicles", "IsCabrio", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.EntityVehicles", "IsCabrio");
        }
    }
}
