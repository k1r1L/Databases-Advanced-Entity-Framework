namespace VehicleInfoSystem.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewTpcStrategy : DbMigration
    {
        public override void Up()
        {
            //RenameColumn(table: "dbo.CruiseShips", name: "PassengersCapacity1", newName: "PassengersCapacity");
            CreateTable(
                "dbo.CargoShips",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        MaxLoadKilograms = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Ships", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.CruiseShips",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        PassengersCapacity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Ships", t => t.Id)
                .Index(t => t.Id);
            
            DropColumn("dbo.Vehicles", "MaxLoadKilograms");
            DropColumn("dbo.Vehicles", "PassengersCapacity1");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Vehicles", "PassengersCapacity1", c => c.Int());
            AddColumn("dbo.Vehicles", "MaxLoadKilograms", c => c.Int());
            DropForeignKey("dbo.CruiseShips", "Id", "dbo.Ships");
            DropForeignKey("dbo.CargoShips", "Id", "dbo.Ships");
            DropIndex("dbo.CruiseShips", new[] { "Id" });
            DropIndex("dbo.CargoShips", new[] { "Id" });
            DropTable("dbo.CruiseShips");
            DropTable("dbo.CargoShips");
            RenameColumn(table: "dbo.CruiseShips", name: "PassengersCapacity", newName: "PassengersCapacity1");
        }
    }
}
