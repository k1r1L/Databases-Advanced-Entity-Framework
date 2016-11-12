namespace _04.SalesDatabase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedPriceColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sales", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sales", "Price");
        }
    }
}
