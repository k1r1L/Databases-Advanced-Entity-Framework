namespace PhotographyWorkshops.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NullableDates : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Workshops", "StartDate", c => c.DateTime());
            AlterColumn("dbo.Workshops", "EndDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Workshops", "EndDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Workshops", "StartDate", c => c.DateTime(nullable: false));
        }
    }
}
