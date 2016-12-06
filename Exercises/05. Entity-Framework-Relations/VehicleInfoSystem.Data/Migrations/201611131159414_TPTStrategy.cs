namespace VehicleInfoSystem.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TPTStrategy : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Vehicles", newName: "Trains");
            DropForeignKey("dbo.Carriages", "TrainId", "dbo.Vehicles");
            DropForeignKey("dbo.Locomotives", "Id", "dbo.Vehicles");
            DropPrimaryKey("dbo.Trains");
            CreateTable(
                "dbo.Vehicles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Manufacturer = c.String(nullable: false),
                        Model = c.String(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MaxSpeed = c.Int(nullable: false),
                        NumberOfEngines = c.Int(),
                        EngineType = c.Int(),
                        TankCapacity = c.Decimal(precision: 18, scale: 2),
                        MaxLoadKilograms = c.Int(),
                        PassengersCapacity1 = c.Int(),
                        ShiftsCount = c.Int(),
                        Color1 = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Cars",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        NumberOfDoors = c.Int(nullable: false),
                        HasInsurance = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Vehicles", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Plains",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        AirlineOwner = c.String(nullable: false),
                        Color = c.String(nullable: false),
                        PassengersCapacity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Vehicles", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Ships",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Nationality = c.String(nullable: false),
                        CaptainName = c.String(nullable: false, maxLength: 50),
                        SizeOfShipCrew = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Vehicles", t => t.Id)
                .Index(t => t.Id);
            
            AlterColumn("dbo.Trains", "Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Trains", "LocomotiveId", c => c.Int(nullable: false));
            AlterColumn("dbo.Trains", "NumberOfCarriages", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Trains", "Id");
            CreateIndex("dbo.Trains", "Id");
            AddForeignKey("dbo.Trains", "Id", "dbo.Vehicles", "Id");
            AddForeignKey("dbo.Carriages", "TrainId", "dbo.Trains", "Id");
            AddForeignKey("dbo.Locomotives", "Id", "dbo.Trains", "Id");
            DropColumn("dbo.Trains", "Manufacturer");
            DropColumn("dbo.Trains", "Model");
            DropColumn("dbo.Trains", "Price");
            DropColumn("dbo.Trains", "MaxSpeed");
            DropColumn("dbo.Trains", "NumberOfEngines");
            DropColumn("dbo.Trains", "EngineType");
            DropColumn("dbo.Trains", "TankCapacity");
            DropColumn("dbo.Trains", "NumberOfDoors");
            DropColumn("dbo.Trains", "HasInsurance");
            DropColumn("dbo.Trains", "AirlineOwner");
            DropColumn("dbo.Trains", "Color");
            DropColumn("dbo.Trains", "PassengersCapacity");
            DropColumn("dbo.Trains", "Nationality");
            DropColumn("dbo.Trains", "CaptainName");
            DropColumn("dbo.Trains", "SizeOfShipCrew");
            DropColumn("dbo.Trains", "MaxLoadKilograms");
            DropColumn("dbo.Trains", "PassengersCapacity1");
            DropColumn("dbo.Trains", "ShiftsCount");
            DropColumn("dbo.Trains", "Color1");
            DropColumn("dbo.Trains", "Discriminator");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Trains", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.Trains", "Color1", c => c.String());
            AddColumn("dbo.Trains", "ShiftsCount", c => c.Int());
            AddColumn("dbo.Trains", "PassengersCapacity1", c => c.Int());
            AddColumn("dbo.Trains", "MaxLoadKilograms", c => c.Int());
            AddColumn("dbo.Trains", "SizeOfShipCrew", c => c.Int());
            AddColumn("dbo.Trains", "CaptainName", c => c.String(maxLength: 50));
            AddColumn("dbo.Trains", "Nationality", c => c.String());
            AddColumn("dbo.Trains", "PassengersCapacity", c => c.Int());
            AddColumn("dbo.Trains", "Color", c => c.String());
            AddColumn("dbo.Trains", "AirlineOwner", c => c.String());
            AddColumn("dbo.Trains", "HasInsurance", c => c.Boolean());
            AddColumn("dbo.Trains", "NumberOfDoors", c => c.Int());
            AddColumn("dbo.Trains", "TankCapacity", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.Trains", "EngineType", c => c.Int());
            AddColumn("dbo.Trains", "NumberOfEngines", c => c.Int());
            AddColumn("dbo.Trains", "MaxSpeed", c => c.Int(nullable: false));
            AddColumn("dbo.Trains", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Trains", "Model", c => c.String(nullable: false));
            AddColumn("dbo.Trains", "Manufacturer", c => c.String(nullable: false));
            DropForeignKey("dbo.Locomotives", "Id", "dbo.Trains");
            DropForeignKey("dbo.Carriages", "TrainId", "dbo.Trains");
            DropForeignKey("dbo.Ships", "Id", "dbo.Vehicles");
            DropForeignKey("dbo.Plains", "Id", "dbo.Vehicles");
            DropForeignKey("dbo.Cars", "Id", "dbo.Vehicles");
            DropForeignKey("dbo.Trains", "Id", "dbo.Vehicles");
            DropIndex("dbo.Ships", new[] { "Id" });
            DropIndex("dbo.Plains", new[] { "Id" });
            DropIndex("dbo.Cars", new[] { "Id" });
            DropIndex("dbo.Trains", new[] { "Id" });
            DropPrimaryKey("dbo.Trains");
            AlterColumn("dbo.Trains", "NumberOfCarriages", c => c.Int());
            AlterColumn("dbo.Trains", "LocomotiveId", c => c.Int());
            AlterColumn("dbo.Trains", "Id", c => c.Int(nullable: false, identity: true));
            DropTable("dbo.Ships");
            DropTable("dbo.Plains");
            DropTable("dbo.Cars");
            DropTable("dbo.Vehicles");
            AddPrimaryKey("dbo.Trains", "Id");
            AddForeignKey("dbo.Locomotives", "Id", "dbo.Vehicles", "Id");
            AddForeignKey("dbo.Carriages", "TrainId", "dbo.Vehicles", "Id", cascadeDelete: true);
            RenameTable(name: "dbo.Trains", newName: "Vehicles");
        }
    }
}
