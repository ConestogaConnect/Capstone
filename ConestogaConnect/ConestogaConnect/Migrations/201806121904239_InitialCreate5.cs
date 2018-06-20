namespace ConestogaConnect.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate5 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.UserRoleModels", newName: "UserRoles");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.UserRoles", newName: "UserRoleModels");
        }
    }
}
