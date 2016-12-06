namespace UniversitySystem.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FuckThisShit : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Teachers", "Email", c => c.String());
            AlterColumn("dbo.People", "FirstName", c => c.String());
            AlterColumn("dbo.People", "LastName", c => c.String());
            AlterColumn("dbo.People", "PhoneNumber", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.People", "PhoneNumber", c => c.String(nullable: false, maxLength: 15));
            AlterColumn("dbo.People", "LastName", c => c.String(nullable: false, maxLength: 25));
            AlterColumn("dbo.People", "FirstName", c => c.String(nullable: false, maxLength: 25));
            AlterColumn("dbo.Teachers", "Email", c => c.String(nullable: false));
        }
    }
}
