namespace UniversitySystem.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewMigration : DbMigration
    {
        public override void Up()
        {
            //CreateTable(
            //    "dbo.People",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            FirstName = c.String(nullable: false, maxLength: 25),
            //            LastName = c.String(nullable: false, maxLength: 25),
            //            PhoneNumber = c.String(nullable: false, maxLength: 15),
            //        })
            //    .PrimaryKey(t => t.Id);
            
            //CreateIndex("dbo.Students", "Id");
            //CreateIndex("dbo.Teachers", "Id");
            //AddForeignKey("dbo.Students", "Id", "dbo.People", "Id");
            //AddForeignKey("dbo.Teachers", "Id", "dbo.People", "Id");
            //DropColumn("dbo.Students", "FirstName");
            //DropColumn("dbo.Students", "LastName");
            //DropColumn("dbo.Students", "PhoneNumber");
            //DropColumn("dbo.Teachers", "FirstName");
            //DropColumn("dbo.Teachers", "LastName");
            //DropColumn("dbo.Teachers", "PhoneNumber");
            //DropTable("dbo.People");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.People",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        FirstName = c.String(nullable: false, maxLength: 25),
                        LastName = c.String(nullable: false, maxLength: 25),
                        PhoneNumber = c.String(nullable: false, maxLength: 15),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Teachers", "PhoneNumber", c => c.String(nullable: false, maxLength: 15));
            AddColumn("dbo.Teachers", "LastName", c => c.String(nullable: false, maxLength: 25));
            AddColumn("dbo.Teachers", "FirstName", c => c.String(nullable: false, maxLength: 25));
            AddColumn("dbo.Students", "PhoneNumber", c => c.String(nullable: false, maxLength: 15));
            AddColumn("dbo.Students", "LastName", c => c.String(nullable: false, maxLength: 25));
            AddColumn("dbo.Students", "FirstName", c => c.String(nullable: false, maxLength: 25));
            DropForeignKey("dbo.Teachers", "Id", "dbo.People");
            DropForeignKey("dbo.Students", "Id", "dbo.People");
            DropIndex("dbo.Teachers", new[] { "Id" });
            DropIndex("dbo.Students", new[] { "Id" });
            DropTable("dbo.People");
        }
    }
}
