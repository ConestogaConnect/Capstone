namespace ConestogaConnect.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "LastName", c => c.String());
            AddColumn("dbo.AspNetUsers", "Program", c => c.String());
            AddColumn("dbo.AspNetUsers", "Semester", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "StudentId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "StudentId");
            DropColumn("dbo.AspNetUsers", "Semester");
            DropColumn("dbo.AspNetUsers", "Program");
            DropColumn("dbo.AspNetUsers", "LastName");
        }
    }
}
