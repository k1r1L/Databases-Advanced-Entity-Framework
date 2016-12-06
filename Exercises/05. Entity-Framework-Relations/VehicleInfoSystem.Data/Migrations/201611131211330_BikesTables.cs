namespace VehicleInfoSystem.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BikesTables : DbMigration
    {
        public override void Up()
        {
            //RenameColumn(table: "dbo.Bikes", name: "Color1", newName: "Color");
            CreateTable(
                "dbo.Bikes",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        ShiftsCount = c.Int(nullable: false),
                        Color = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Vehicles", t => t.Id)
                .Index(t => t.Id);
            
            AlterColumn("dbo.Vehicles", "Discriminator", c => c.String(maxLength: 128));
            DropColumn("dbo.Vehicles", "ShiftsCount");
            DropColumn("dbo.Vehicles", "Color1");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Vehicles", "Color1", c => c.String());
            AddColumn("dbo.Vehicles", "ShiftsCount", c => c.Int());
            DropForeignKey("dbo.Bikes", "Id", "dbo.Vehicles");
            DropIndex("dbo.Bikes", new[] { "Id" });
            AlterColumn("dbo.Vehicles", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            DropTable("dbo.Bikes");
            RenameColumn(table: "dbo.Bikes", name: "Color", newName: "Color1");
        }
    }
}
