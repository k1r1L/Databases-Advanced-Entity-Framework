namespace _04.SalesDatabase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NoLongerRequired : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Sales", "Customer_CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Sales", "Product_ProductId", "dbo.Products");
            DropForeignKey("dbo.Sales", "StoreLocation_StoreLocationId", "dbo.StoreLocations");
            DropIndex("dbo.Sales", new[] { "Customer_CustomerId" });
            DropIndex("dbo.Sales", new[] { "Product_ProductId" });
            DropIndex("dbo.Sales", new[] { "StoreLocation_StoreLocationId" });
            AlterColumn("dbo.Sales", "Customer_CustomerId", c => c.Int());
            AlterColumn("dbo.Sales", "Product_ProductId", c => c.Int());
            AlterColumn("dbo.Sales", "StoreLocation_StoreLocationId", c => c.Int());
            CreateIndex("dbo.Sales", "Customer_CustomerId");
            CreateIndex("dbo.Sales", "Product_ProductId");
            CreateIndex("dbo.Sales", "StoreLocation_StoreLocationId");
            AddForeignKey("dbo.Sales", "Customer_CustomerId", "dbo.Customers", "CustomerId");
            AddForeignKey("dbo.Sales", "Product_ProductId", "dbo.Products", "ProductId");
            AddForeignKey("dbo.Sales", "StoreLocation_StoreLocationId", "dbo.StoreLocations", "StoreLocationId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sales", "StoreLocation_StoreLocationId", "dbo.StoreLocations");
            DropForeignKey("dbo.Sales", "Product_ProductId", "dbo.Products");
            DropForeignKey("dbo.Sales", "Customer_CustomerId", "dbo.Customers");
            DropIndex("dbo.Sales", new[] { "StoreLocation_StoreLocationId" });
            DropIndex("dbo.Sales", new[] { "Product_ProductId" });
            DropIndex("dbo.Sales", new[] { "Customer_CustomerId" });
            AlterColumn("dbo.Sales", "StoreLocation_StoreLocationId", c => c.Int(nullable: false));
            AlterColumn("dbo.Sales", "Product_ProductId", c => c.Int(nullable: false));
            AlterColumn("dbo.Sales", "Customer_CustomerId", c => c.Int(nullable: false));
            CreateIndex("dbo.Sales", "StoreLocation_StoreLocationId");
            CreateIndex("dbo.Sales", "Product_ProductId");
            CreateIndex("dbo.Sales", "Customer_CustomerId");
            AddForeignKey("dbo.Sales", "StoreLocation_StoreLocationId", "dbo.StoreLocations", "StoreLocationId", cascadeDelete: true);
            AddForeignKey("dbo.Sales", "Product_ProductId", "dbo.Products", "ProductId", cascadeDelete: true);
            AddForeignKey("dbo.Sales", "Customer_CustomerId", "dbo.Customers", "CustomerId", cascadeDelete: true);
        }
    }
}
