namespace _04.SalesDatabase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProductNameLength : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Sales");
            AlterColumn("dbo.Sales", "SaleId", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Products", "Name", c => c.String(nullable: false, maxLength: 250));
            AddPrimaryKey("dbo.Sales", "SaleId");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Sales");
            AlterColumn("dbo.Products", "Name", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Sales", "SaleId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Sales", "PriceOfSale");
        }
    }
}
