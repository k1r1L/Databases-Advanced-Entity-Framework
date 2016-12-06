namespace UniversitySystem.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Students", "Id", "dbo.People");
            DropForeignKey("dbo.Teachers", "Id", "dbo.People");
            DropPrimaryKey("dbo.People");
            AlterColumn("dbo.People", "Id", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.People", "Id");
            AddForeignKey("dbo.Students", "Id", "dbo.People", "Id");
            AddForeignKey("dbo.Teachers", "Id", "dbo.People", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Teachers", "Id", "dbo.People");
            DropForeignKey("dbo.Students", "Id", "dbo.People");
            DropPrimaryKey("dbo.People");
            AlterColumn("dbo.People", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.People", "Id");
            AddForeignKey("dbo.Teachers", "Id", "dbo.People", "Id");
            AddForeignKey("dbo.Students", "Id", "dbo.People", "Id");
        }
    }
}
