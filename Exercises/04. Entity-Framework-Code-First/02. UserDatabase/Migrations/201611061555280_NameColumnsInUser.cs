namespace _02.UserDatabase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NameColumnsInUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "FirstName", c => c.String(nullable: false, defaultValue: "No"));
            AddColumn("dbo.Users", "LastName", c => c.String(nullable: false, defaultValue: "Name"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "LastName");
            DropColumn("dbo.Users", "FirstName");
        }
    }
}
