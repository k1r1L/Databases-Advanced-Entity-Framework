namespace _05.HospitalDatabase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedEmailPasswordColumns : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Doctors", "Email", c => c.String(nullable: false, defaultValue: "email@abv.bg"));
            AddColumn("dbo.Doctors", "Password", c => c.String(nullable: false, maxLength: 60, defaultValue: "1234"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Doctors", "Password");
            DropColumn("dbo.Doctors", "Email");
        }
    }
}
