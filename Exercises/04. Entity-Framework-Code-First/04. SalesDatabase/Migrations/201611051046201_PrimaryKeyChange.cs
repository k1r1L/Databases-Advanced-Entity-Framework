namespace _04.SalesDatabase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PrimaryKeyChange : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Sales");
            AlterColumn("dbo.Sales", "SaleId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Sales", "PriceOfSale");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Sales");
            AlterColumn("dbo.Sales", "SaleId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Sales", "SaleId");
        }
    }
}
