namespace PhotographyWorkshops.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accessories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Owner_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Photographers", t => t.Owner_Id)
                .Index(t => t.Owner_Id);
            
            CreateTable(
                "dbo.Photographers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false, maxLength: 50),
                        PhoneNumber = c.String(),
                        PrimaryCamera_Id = c.Int(nullable: false),
                        SecondaryCamera_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cameras", t => t.PrimaryCamera_Id)
                .ForeignKey("dbo.Cameras", t => t.SecondaryCamera_Id)
                .Index(t => t.PrimaryCamera_Id)
                .Index(t => t.SecondaryCamera_Id);
            
            CreateTable(
                "dbo.Lenses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Make = c.String(),
                        FocalLength = c.Int(nullable: false),
                        MaxAperture = c.Decimal(nullable: false, precision: 16, scale: 1),
                        CompatibleWith = c.String(),
                        Owner_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Photographers", t => t.Owner_Id)
                .Index(t => t.Owner_Id);
            
            CreateTable(
                "dbo.Workshops",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        Location = c.String(nullable: false),
                        PricePerParticipant = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Trainer_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Photographers", t => t.Trainer_Id)
                .Index(t => t.Trainer_Id);
            
            CreateTable(
                "dbo.Cameras",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Model = c.String(nullable: false),
                        Make = c.String(nullable: false),
                        MinIso = c.Int(nullable: false),
                        MaxIso = c.Int(nullable: false),
                        IsFullFrame = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.WorkshopParticipants",
                c => new
                    {
                        WorkshopId = c.Int(nullable: false),
                        ParticipantId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.WorkshopId, t.ParticipantId })
                .ForeignKey("dbo.Workshops", t => t.WorkshopId, cascadeDelete: true)
                .ForeignKey("dbo.Photographers", t => t.ParticipantId, cascadeDelete: true)
                .Index(t => t.WorkshopId)
                .Index(t => t.ParticipantId);
            
            CreateTable(
                "dbo.DslrCameras",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        MaxShutterSpeed = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cameras", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.MirrorlessCameras",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        MaxVideoResolution = c.String(),
                        MaxFrameRate = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cameras", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MirrorlessCameras", "Id", "dbo.Cameras");
            DropForeignKey("dbo.DslrCameras", "Id", "dbo.Cameras");
            DropForeignKey("dbo.Photographers", "SecondaryCamera_Id", "dbo.Cameras");
            DropForeignKey("dbo.Photographers", "PrimaryCamera_Id", "dbo.Cameras");
            DropForeignKey("dbo.Workshops", "Trainer_Id", "dbo.Photographers");
            DropForeignKey("dbo.WorkshopParticipants", "ParticipantId", "dbo.Photographers");
            DropForeignKey("dbo.WorkshopParticipants", "WorkshopId", "dbo.Workshops");
            DropForeignKey("dbo.Lenses", "Owner_Id", "dbo.Photographers");
            DropForeignKey("dbo.Accessories", "Owner_Id", "dbo.Photographers");
            DropIndex("dbo.MirrorlessCameras", new[] { "Id" });
            DropIndex("dbo.DslrCameras", new[] { "Id" });
            DropIndex("dbo.WorkshopParticipants", new[] { "ParticipantId" });
            DropIndex("dbo.WorkshopParticipants", new[] { "WorkshopId" });
            DropIndex("dbo.Workshops", new[] { "Trainer_Id" });
            DropIndex("dbo.Lenses", new[] { "Owner_Id" });
            DropIndex("dbo.Photographers", new[] { "SecondaryCamera_Id" });
            DropIndex("dbo.Photographers", new[] { "PrimaryCamera_Id" });
            DropIndex("dbo.Accessories", new[] { "Owner_Id" });
            DropTable("dbo.MirrorlessCameras");
            DropTable("dbo.DslrCameras");
            DropTable("dbo.WorkshopParticipants");
            DropTable("dbo.Cameras");
            DropTable("dbo.Workshops");
            DropTable("dbo.Lenses");
            DropTable("dbo.Photographers");
            DropTable("dbo.Accessories");
        }
    }
}
