namespace BillPaymentSystem.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BillingDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Number = c.String(nullable: false),
                        OwnerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.OwnerId, cascadeDelete: true)
                .Index(t => t.OwnerId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        Email = c.String(nullable: false),
                        Password = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CreditCards",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        CardType = c.String(nullable: false),
                        ExpirationMonth = c.Int(nullable: false),
                        ExpirationYear = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BillingDetails", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.BankAccounts",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        BankName = c.String(nullable: false),
                        SwiftCode = c.String(nullable: false, maxLength: 11),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BillingDetails", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BankAccounts", "Id", "dbo.BillingDetails");
            DropForeignKey("dbo.CreditCards", "Id", "dbo.BillingDetails");
            DropForeignKey("dbo.BillingDetails", "OwnerId", "dbo.Users");
            DropIndex("dbo.BankAccounts", new[] { "Id" });
            DropIndex("dbo.CreditCards", new[] { "Id" });
            DropIndex("dbo.BillingDetails", new[] { "OwnerId" });
            DropTable("dbo.BankAccounts");
            DropTable("dbo.CreditCards");
            DropTable("dbo.Users");
            DropTable("dbo.BillingDetails");
        }
    }
}
