namespace BankSystem.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        Email = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.BankingAccounts", "OwnerId", c => c.Int(nullable: false));
            CreateIndex("dbo.BankingAccounts", "OwnerId");
            AddForeignKey("dbo.BankingAccounts", "OwnerId", "dbo.Users", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BankingAccounts", "OwnerId", "dbo.Users");
            DropIndex("dbo.BankingAccounts", new[] { "OwnerId" });
            DropColumn("dbo.BankingAccounts", "OwnerId");
            DropTable("dbo.Users");
        }
    }
}
