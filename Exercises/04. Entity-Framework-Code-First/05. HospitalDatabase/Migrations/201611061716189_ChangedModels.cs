namespace _05.HospitalDatabase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedModels : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Diagnoses", name: "Patient_FirstName", newName: "Patients_FirstName");
            RenameColumn(table: "dbo.Diagnoses", name: "Patient_LastName", newName: "Patients_LastName");
            RenameIndex(table: "dbo.Diagnoses", name: "IX_Patient_FirstName_Patient_LastName", newName: "IX_Patients_FirstName_Patients_LastName");
            DropPrimaryKey("dbo.Diagnoses");
            DropPrimaryKey("dbo.Medicaments");
            AddColumn("dbo.Diagnoses", "DiagnoseId", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Medicaments", "MedicamentId", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Diagnoses", "Name", c => c.String(maxLength: 50));
            AlterColumn("dbo.Medicaments", "Name", c => c.String(maxLength: 50));
            AddPrimaryKey("dbo.Diagnoses", "DiagnoseId");
            AddPrimaryKey("dbo.Medicaments", "MedicamentId");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Medicaments");
            DropPrimaryKey("dbo.Diagnoses");
            AlterColumn("dbo.Medicaments", "Name", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Diagnoses", "Name", c => c.String(nullable: false, maxLength: 50));
            DropColumn("dbo.Medicaments", "MedicamentId");
            DropColumn("dbo.Diagnoses", "DiagnoseId");
            AddPrimaryKey("dbo.Medicaments", "Name");
            AddPrimaryKey("dbo.Diagnoses", "Name");
            RenameIndex(table: "dbo.Diagnoses", name: "IX_Patients_FirstName_Patients_LastName", newName: "IX_Patient_FirstName_Patient_LastName");
            RenameColumn(table: "dbo.Diagnoses", name: "Patients_LastName", newName: "Patient_LastName");
            RenameColumn(table: "dbo.Diagnoses", name: "Patients_FirstName", newName: "Patient_FirstName");
        }
    }
}
