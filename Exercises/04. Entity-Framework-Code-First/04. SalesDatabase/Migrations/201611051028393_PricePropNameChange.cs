namespace _04.SalesDatabase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PricePropNameChange : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sales", "PriceOfSale", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.Sales", "Price");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Sales", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.Sales", "PriceOfSale");
        }
    }
}
