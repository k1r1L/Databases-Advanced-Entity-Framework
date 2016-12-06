namespace BankSystem.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BankingAccounts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AccountNumber = c.String(nullable: false, maxLength: 10),
                        Balance = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CheckingAccounts",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Fee = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BankingAccounts", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.SavingAccounts",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        InterestRate = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BankingAccounts", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SavingAccounts", "Id", "dbo.BankingAccounts");
            DropForeignKey("dbo.CheckingAccounts", "Id", "dbo.BankingAccounts");
            DropIndex("dbo.SavingAccounts", new[] { "Id" });
            DropIndex("dbo.CheckingAccounts", new[] { "Id" });
            DropTable("dbo.SavingAccounts");
            DropTable("dbo.CheckingAccounts");
            DropTable("dbo.BankingAccounts");
        }
    }
}
