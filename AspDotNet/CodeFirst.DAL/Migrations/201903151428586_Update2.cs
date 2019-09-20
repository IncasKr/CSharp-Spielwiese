namespace CodeFirst.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update2 : DbMigration
    {
        public override void Up()
        {
            RenameColumn("dbo.EntityVehicles", "Turbo", "HasTurbo");            
        }
        
        public override void Down()
        {
            RenameColumn("dbo.EntityVehicles", "HasTurbo", "Turbo");
        }
    }
}
