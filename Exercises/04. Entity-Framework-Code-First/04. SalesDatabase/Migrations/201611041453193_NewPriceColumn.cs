namespace _04.SalesDatabase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewPriceColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "NewPrice", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "NewPrice");
        }
    }
}
