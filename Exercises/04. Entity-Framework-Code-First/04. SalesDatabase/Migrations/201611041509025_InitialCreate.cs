namespace _04.SalesDatabase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        CustomerId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Email = c.String(nullable: false),
                        CreditCardNumber = c.String(maxLength: 20, unicode: false),
                    })
                .PrimaryKey(t => t.CustomerId);
            
            CreateTable(
                "dbo.Sales",
                c => new
                    {
                        SaleId = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Customer_CustomerId = c.Int(nullable: false),
                        Product_ProductId = c.Int(nullable: false),
                        StoreLocation_StoreLocationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SaleId)
                .ForeignKey("dbo.Customers", t => t.Customer_CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.Product_ProductId, cascadeDelete: true)
                .ForeignKey("dbo.StoreLocations", t => t.StoreLocation_StoreLocationId, cascadeDelete: true)
                .Index(t => t.Customer_CustomerId)
                .Index(t => t.Product_ProductId)
                .Index(t => t.StoreLocation_StoreLocationId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Quantity = c.Double(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ProductId);
            
            CreateTable(
                "dbo.StoreLocations",
                c => new
                    {
                        StoreLocationId = c.Int(nullable: false, identity: true),
                        LocationName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.StoreLocationId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sales", "StoreLocation_StoreLocationId", "dbo.StoreLocations");
            DropForeignKey("dbo.Sales", "Product_ProductId", "dbo.Products");
            DropForeignKey("dbo.Sales", "Customer_CustomerId", "dbo.Customers");
            DropIndex("dbo.Sales", new[] { "StoreLocation_StoreLocationId" });
            DropIndex("dbo.Sales", new[] { "Product_ProductId" });
            DropIndex("dbo.Sales", new[] { "Customer_CustomerId" });
            DropTable("dbo.StoreLocations");
            DropTable("dbo.Products");
            DropTable("dbo.Sales");
            DropTable("dbo.Customers");
        }
    }
}
