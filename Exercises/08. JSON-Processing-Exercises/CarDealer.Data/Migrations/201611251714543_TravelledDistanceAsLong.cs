namespace CarDealer.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TravelledDistanceAsLong : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Cars", "TravelledDistance", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Cars", "TravelledDistance", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
