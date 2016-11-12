namespace _05.HospitalDatabase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Diagnoses",
                c => new
                    {
                        Name = c.String(nullable: false, maxLength: 50),
                        Comments = c.String(maxLength: 500),
                        Patient_FirstName = c.String(maxLength: 25),
                        Patient_LastName = c.String(maxLength: 25),
                    })
                .PrimaryKey(t => t.Name)
                .ForeignKey("dbo.Patients", t => new { t.Patient_FirstName, t.Patient_LastName })
                .Index(t => new { t.Patient_FirstName, t.Patient_LastName });
            
            CreateTable(
                "dbo.Patients",
                c => new
                    {
                        FirstName = c.String(nullable: false, maxLength: 25),
                        LastName = c.String(nullable: false, maxLength: 25),
                        Address = c.String(nullable: false),
                        Email = c.String(),
                        DateOfBirth = c.DateTime(nullable: false),
                        Picture = c.Binary(),
                        HasMedicalInsurance = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.FirstName, t.LastName });
            
            CreateTable(
                "dbo.Medicaments",
                c => new
                    {
                        Name = c.String(nullable: false, maxLength: 50),
                        Patient_FirstName = c.String(maxLength: 25),
                        Patient_LastName = c.String(maxLength: 25),
                    })
                .PrimaryKey(t => t.Name)
                .ForeignKey("dbo.Patients", t => new { t.Patient_FirstName, t.Patient_LastName })
                .Index(t => new { t.Patient_FirstName, t.Patient_LastName });
            
            CreateTable(
                "dbo.Visitations",
                c => new
                    {
                        VisitationId = c.Int(nullable: false, identity: true),
                        Time = c.DateTime(nullable: false),
                        Comments = c.String(maxLength: 500),
                        Doctor_DoctorId = c.Int(),
                        Patient_FirstName = c.String(maxLength: 25),
                        Patient_LastName = c.String(maxLength: 25),
                    })
                .PrimaryKey(t => t.VisitationId)
                .ForeignKey("dbo.Doctors", t => t.Doctor_DoctorId)
                .ForeignKey("dbo.Patients", t => new { t.Patient_FirstName, t.Patient_LastName })
                .Index(t => t.Doctor_DoctorId)
                .Index(t => new { t.Patient_FirstName, t.Patient_LastName });
            
            CreateTable(
                "dbo.Doctors",
                c => new
                    {
                        DoctorId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Specialty = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.DoctorId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Visitations", new[] { "Patient_FirstName", "Patient_LastName" }, "dbo.Patients");
            DropForeignKey("dbo.Visitations", "Doctor_DoctorId", "dbo.Doctors");
            DropForeignKey("dbo.Medicaments", new[] { "Patient_FirstName", "Patient_LastName" }, "dbo.Patients");
            DropForeignKey("dbo.Diagnoses", new[] { "Patient_FirstName", "Patient_LastName" }, "dbo.Patients");
            DropIndex("dbo.Visitations", new[] { "Patient_FirstName", "Patient_LastName" });
            DropIndex("dbo.Visitations", new[] { "Doctor_DoctorId" });
            DropIndex("dbo.Medicaments", new[] { "Patient_FirstName", "Patient_LastName" });
            DropIndex("dbo.Diagnoses", new[] { "Patient_FirstName", "Patient_LastName" });
            DropTable("dbo.Doctors");
            DropTable("dbo.Visitations");
            DropTable("dbo.Medicaments");
            DropTable("dbo.Patients");
            DropTable("dbo.Diagnoses");
        }
    }
}
