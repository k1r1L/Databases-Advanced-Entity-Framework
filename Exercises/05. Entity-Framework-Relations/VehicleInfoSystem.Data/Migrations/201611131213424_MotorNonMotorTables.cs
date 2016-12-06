namespace VehicleInfoSystem.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MotorNonMotorTables : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Trains", "Id", "dbo.Vehicles");
            DropForeignKey("dbo.Bikes", "Id", "dbo.Vehicles");
            DropForeignKey("dbo.Cars", "Id", "dbo.Vehicles");
            DropForeignKey("dbo.Plains", "Id", "dbo.Vehicles");
            DropForeignKey("dbo.Ships", "Id", "dbo.Vehicles");
            CreateTable(
                "dbo.MotorVehicles",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        NumberOfEngines = c.Int(nullable: false),
                        EngineType = c.Int(nullable: false),
                        TankCapacity = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Vehicles", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.NonMotorVehicles",
                c => new
                    {
                        Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Vehicles", t => t.Id)
                .Index(t => t.Id);
            
            AddForeignKey("dbo.Trains", "Id", "dbo.MotorVehicles", "Id");
            AddForeignKey("dbo.Bikes", "Id", "dbo.NonMotorVehicles", "Id");
            AddForeignKey("dbo.Cars", "Id", "dbo.MotorVehicles", "Id");
            AddForeignKey("dbo.Plains", "Id", "dbo.MotorVehicles", "Id");
            AddForeignKey("dbo.Ships", "Id", "dbo.MotorVehicles", "Id");
            DropColumn("dbo.Vehicles", "NumberOfEngines");
            DropColumn("dbo.Vehicles", "EngineType");
            DropColumn("dbo.Vehicles", "TankCapacity");
            DropColumn("dbo.Vehicles", "Discriminator");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Vehicles", "Discriminator", c => c.String(maxLength: 128));
            AddColumn("dbo.Vehicles", "TankCapacity", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.Vehicles", "EngineType", c => c.Int());
            AddColumn("dbo.Vehicles", "NumberOfEngines", c => c.Int());
            DropForeignKey("dbo.Ships", "Id", "dbo.MotorVehicles");
            DropForeignKey("dbo.Plains", "Id", "dbo.MotorVehicles");
            DropForeignKey("dbo.Cars", "Id", "dbo.MotorVehicles");
            DropForeignKey("dbo.Bikes", "Id", "dbo.NonMotorVehicles");
            DropForeignKey("dbo.NonMotorVehicles", "Id", "dbo.Vehicles");
            DropForeignKey("dbo.Trains", "Id", "dbo.MotorVehicles");
            DropForeignKey("dbo.MotorVehicles", "Id", "dbo.Vehicles");
            DropIndex("dbo.NonMotorVehicles", new[] { "Id" });
            DropIndex("dbo.MotorVehicles", new[] { "Id" });
            DropTable("dbo.NonMotorVehicles");
            DropTable("dbo.MotorVehicles");
            AddForeignKey("dbo.Ships", "Id", "dbo.Vehicles", "Id");
            AddForeignKey("dbo.Plains", "Id", "dbo.Vehicles", "Id");
            AddForeignKey("dbo.Cars", "Id", "dbo.Vehicles", "Id");
            AddForeignKey("dbo.Bikes", "Id", "dbo.Vehicles", "Id");
            AddForeignKey("dbo.Trains", "Id", "dbo.Vehicles", "Id");
        }
    }
}
