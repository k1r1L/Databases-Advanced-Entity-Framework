namespace UniversitySystem.Data.Migrations
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Migrations;
    using System.IO;
    using System.Linq;
    using System.Text;
    internal sealed class Configuration : DbMigrationsConfiguration<UniversitySystem.Data.UniversitySystemContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "UniversitySystem.Data.UniversitySystemContext";
        }

        protected override void Seed(UniversitySystemContext context)
        {
            StreamReader reader = new StreamReader(@"C:\Data Kiril\SoftUni\DB Fundamentals\Databases Advanced (Entity Framework)\Exercises\Entity-Framework-Relations\universitySystemSeedData.txt");
            string line = reader.ReadLine();
            int id = 1;

            if (context.People.Any())
            {
                id = context.People.OrderByDescending(p => p.Id).First().Id + 1;
            }

            line = reader.ReadLine();
            while (line != "Students")
            {
                
                string[] lineInfo = line.Split();
                string firstName = lineInfo[0];
                string lastName = lineInfo[1];
                string phoneNumber = lineInfo[2];
                string email = lineInfo[3];
                decimal salaryPerHour = decimal.Parse(lineInfo[4]);

                context.People.AddOrUpdate(p => p.PhoneNumber, new Teacher()
                {
                    Id = ++id,
                    FirstName = firstName,
                    LastName = lastName,
                    PhoneNumber = phoneNumber,
                    Email = email,
                    SalaryPerHour = salaryPerHour,
                });

                line = reader.ReadLine();
            }

            context.SaveChanges();
            line = reader.ReadLine();
            while (line != null)
            {
                string[] lineInfo = line.Split();
                string firstName = lineInfo[0];
                string lastName = lineInfo[1];
                string phoneNumber = lineInfo[2];
                decimal avgGrade = decimal.Parse(lineInfo[3]);
                Attendance attendance = (Attendance)Enum.Parse(typeof(Attendance), lineInfo[4]);

                context.People.AddOrUpdate(p => p.PhoneNumber, new Student()
                {
                    Id = ++id,
                    FirstName = firstName,
                    LastName = lastName,
                    PhoneNumber = phoneNumber,
                    AverageGrade = avgGrade,
                    Attendance = attendance
                });

                line = reader.ReadLine();
            }

            context.SaveChanges();
        }
    }
}
