namespace _05.HospitalDatabase.Migrations
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<_05.HospitalDatabase.HospitalContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "_05.HospitalDatabase.HospitalContext";
        }

        protected override void Seed(HospitalContext db)
        {
            if (db.Patients.Any())
            {
                return;
            }

            Patient kiril = new Patient()
            {
                FirstName = "Kiril",
                LastName = "Kirilov",
                Address = "j.k suha reka",
                Email = "kiril@gmail.com",
                DateOfBirth = new DateTime(1998, 01, 06),
                HasMedicalInsurance = true
            };

            Patient valentin = new Patient()
            {
                FirstName = "Valentin",
                LastName = "Vitkov",
                Address = "j.k suha reka",
                Email = "valentin@gmail.com",
                DateOfBirth = new DateTime(1969, 12, 10),
                HasMedicalInsurance = true
            };

            Patient zezka = new Patient()
            {
                FirstName = "Tsvetanka",
                LastName = "Vitkov",
                Address = "j.k suha reka",
                Email = "zezka@gmail.com",
                DateOfBirth = new DateTime(1970, 04, 01),
                HasMedicalInsurance = true
            };

            Diagnose d1 = new Diagnose()
            {
                Name = "ovi",
                Comments = "Healable",
                Patient = kiril
            };

            Diagnose d2 = new Diagnose()
            {
                Name = "flu",
                Comments = "Healable",
                Patient = zezka
            };

            Diagnose d3 = new Diagnose()
            {
                Name = "cancer",
                Comments = "Not healable",
                Patient = valentin
            };

            Medicament m1 = new Medicament()
            {
                Name = "Lekarstvo protiv ovi",
                Patient = kiril
            };

            Medicament m2 = new Medicament()
            {
                Name = "Lekarstvo protiv grip",
                Patient = zezka
            };

            Medicament m3 = new Medicament()
            {
                Name = "Weed",
                Patient = valentin
            };

            Doctor vladi = new Doctor()
            {
                Name = "Vladimira",
                Specialty = "Everything",
                Email = "vladimira@abv.bg",
                Password = "azsumdoktor"
            };

            Visitation kirilVisitation = new Visitation()
            {
                Time = new DateTime(2016, 06, 22),
                Comments = "Kiril's visitation",
                Doctor = vladi,
                Patient = kiril
            };

            Visitation valentinVisitation = new Visitation()
            {
                Time = new DateTime(2016, 07, 22),
                Comments = "Valentin's visitation",
                Doctor = vladi,
                Patient = valentin
            };

            Visitation zezkaVisitation = new Visitation()
            {
                Time = new DateTime(2016, 08, 22),
                Comments = "Zezka's visitation",
                Doctor = vladi,
                Patient = zezka
            };

            kiril.Visitations.Add(kirilVisitation);
            zezka.Visitations.Add(zezkaVisitation);
            valentin.Visitations.Add(valentinVisitation);

            kiril.Diagnoses.Add(d1);
            zezka.Diagnoses.Add(d2);
            valentin.Diagnoses.Add(d3);

            kiril.Medicaments.Add(m1);
            zezka.Medicaments.Add(m2);
            valentin.Medicaments.Add(m3);

            vladi.Visitations.Add(kirilVisitation);
            vladi.Visitations.Add(valentinVisitation);
            vladi.Visitations.Add(zezkaVisitation);

            db.Medicaments.Add(m1);
            db.Medicaments.Add(m2);
            db.Medicaments.Add(m3);

            db.Visitations.Add(kirilVisitation);
            db.Visitations.Add(zezkaVisitation);
            db.Visitations.Add(valentinVisitation);

            db.Patients.Add(kiril);
            db.Patients.Add(zezka);
            db.Patients.Add(valentin);

            db.Diagnoses.Add(d1);
            db.Diagnoses.Add(d2);
            db.Diagnoses.Add(d3);

            db.Doctors.Add(vladi);

            base.Seed(db);
        }
    }
}
