namespace _05.HospitalDatabase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedPatientsColumnName : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Diagnoses", name: "Patients_FirstName", newName: "Patient_FirstName");
            RenameColumn(table: "dbo.Diagnoses", name: "Patients_LastName", newName: "Patient_LastName");
            RenameIndex(table: "dbo.Diagnoses", name: "IX_Patients_FirstName_Patients_LastName", newName: "IX_Patient_FirstName_Patient_LastName");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Diagnoses", name: "IX_Patient_FirstName_Patient_LastName", newName: "IX_Patients_FirstName_Patients_LastName");
            RenameColumn(table: "dbo.Diagnoses", name: "Patient_LastName", newName: "Patients_LastName");
            RenameColumn(table: "dbo.Diagnoses", name: "Patient_FirstName", newName: "Patients_FirstName");
        }
    }
}
