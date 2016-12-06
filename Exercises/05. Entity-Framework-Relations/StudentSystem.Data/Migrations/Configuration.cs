namespace StudentSystem.Data.Migrations
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.IO;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<StudentSystem.Data.StudentSystemContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "StudentSystem.Data.StudentSystemContext";
        }

        protected override void Seed(StudentSystemContext context)
        {
            List<Student> studentsToAdd = new List<Student>();
            context.Students.AddOrUpdate(s => s.Name, new Student()
            {
                Name = "Kiril",
                PhoneNumber = "0895964686",
                RegistrationDate = DateTime.Now,
                Birthday = new DateTime(1998, 01, 06),
            });

            context.Students.AddOrUpdate(s => s.Name, new Student()
            {
                Name = "Slav",
                PhoneNumber = "0895964686",
                RegistrationDate = DateTime.Now,
                Birthday = new DateTime(1997, 03, 17)
            });

            context.Students.AddOrUpdate(s => s.Name, new Student()
            {
                Name = "Georgi",
                PhoneNumber = "0895964686",
                RegistrationDate = DateTime.Now,
                Birthday = new DateTime(1996, 03, 04)
            });

            context.Students.AddOrUpdate(s => s.Name, new Student()
            {
                Name = "Duci",
                PhoneNumber = "0895964686",
                RegistrationDate = DateTime.Now,
                Birthday = new DateTime(1995, 06, 21)
            });

            context.Students.AddOrUpdate(s => s.Name, new Student()
            {
                Name = "Mici",
                PhoneNumber = "0895964686",
                RegistrationDate = DateTime.Now,
                Birthday = new DateTime(1992, 01, 01)
            });
            Student kiril = context.Students.Find(1);
            Student slav = context.Students.Find(2);
            Student georgi = context.Students.Find(3);
            Student duci = context.Students.Find(4);
            Student mici = context.Students.Find(5);

            studentsToAdd.Add(kiril);
            studentsToAdd.Add(slav);
            studentsToAdd.Add(georgi);
            studentsToAdd.Add(duci);
            studentsToAdd.Add(mici);

            context.Courses.AddOrUpdate(c => c.Name, new Course
            {
                Name = "Databases Basics",
                Description = "First course from DB Fundamentals",
                StartDate = new DateTime(2016, 09, 19),
                EndDate = new DateTime(2016, 10, 16),
                Price = 180m,
            });

            context.Courses.AddOrUpdate(c => c.Name, new Course
            {
                Name = "Databases Advanced",
                Description = "Second course from DB Fundamentals",
                StartDate = new DateTime(2016, 10, 17),
                EndDate = new DateTime(2016, 12, 11),
                Price = 180m,
            });

            context.Courses.AddOrUpdate(c => c.Name, new Course
            {
                Name = "Software Technologies",
                Description = "Second course from Tech Module",
                StartDate = new DateTime(2016, 10, 17),
                EndDate = new DateTime(2016, 12, 20),
                Price = 160m,
            });

            context.SaveChanges();
            Course dbBasics = context.Courses.Find(1);
            Course dbAdvanced = context.Courses.Find(2);
            Course softwareTechnologies = context.Courses.Find(3);

            if (dbBasics.Students.Count == 0)
            {
                dbBasics.Students = studentsToAdd;
                dbAdvanced.Students = studentsToAdd;
                softwareTechnologies.Students = studentsToAdd;
                context.SaveChanges();
            }

            List<Course> coursesToAdd = new List<Course>()
            {
                dbBasics,
                dbAdvanced,
                softwareTechnologies
            };

            context.Resources.AddOrUpdate(r => r.Name, new Resource()
            {
                Name = "Course Schedule for dbBasics",
                ResourceType = ResourceType.Document,
                Url = "www.softuni.bg/Courses/Databases-Basics",
                Course = coursesToAdd[0]
            });


            context.Resources.AddOrUpdate(r => r.Name, new Resource()
            {
                Name = "Course Schedule for dbAdvanced",
                ResourceType = ResourceType.Document,
                Url = "www.softuni.bg/Courses/Databases-Advanced",
                Course = coursesToAdd[1]
            });

            context.Resources.AddOrUpdate(r => r.Name, new Resource()
            {
                Name = "Course Schedule for software technologies",
                ResourceType = ResourceType.Document,
                Url = "www.softuni.bg/Courses/Software-Technologies",
                Course = coursesToAdd[2]
            });

            context.Resources.AddOrUpdate(r => r.Name, new Resource()
            {
                Name = "Lab for db basics",
                ResourceType = ResourceType.Other,
                Url = "www.softuni.bg/Courses/Databases-Basics",
                Course = coursesToAdd[0]
            });

            context.Resources.AddOrUpdate(r => r.Name, new Resource()
            {
                Name = "Lab for db advanced",
                ResourceType = ResourceType.Other,
                Url = "www.softuni.bg/Courses/Databases-Advanced",
                Course = coursesToAdd[1]
            });

            context.Resources.AddOrUpdate(r => r.Name, new Resource()
            {
                Name = "Lab for software technologies",
                ResourceType = ResourceType.Other,
                Url = "www.softuni.bg/Courses/Software-Technologies",
                Course = coursesToAdd[2]
            });

            context.Resources.AddOrUpdate(r => r.Name, new Resource()
            {
                Name = "Opening video for db basics",
                ResourceType = ResourceType.Video,
                Url = "www.softuni.bg/Courses/Databases-Basics",
                Course = coursesToAdd[0]
            });

            context.Resources.AddOrUpdate(r => r.Name, new Resource()
            {
                Name = "Opening video for db advanced",
                ResourceType = ResourceType.Video,
                Url = "www.softuni.bg/Courses/Databases-Advanced",
                Course = coursesToAdd[1]
            });

            context.Resources.AddOrUpdate(r => r.Name, new Resource()
            {
                Name = "Opening video for software technologies",
                ResourceType = ResourceType.Video,
                Url = "www.softuni.bg/Courses/Software-Technologies",
                Course = coursesToAdd[2]
            });

            context.SaveChanges();
            context.Homeworks.AddOrUpdate(h => h.SubmissionDate, new Homework()
            {
                Content = "CRUD",
                ContentType = ContentType.Zip,
                SubmissionDate = new DateTime(2016, 09, 24),
                Course = coursesToAdd[0],
                Student = studentsToAdd[0]
            });

            context.Homeworks.AddOrUpdate(h => h.SubmissionDate, new Homework()
            {
                Content = "CRUD",
                ContentType = ContentType.Zip,
                SubmissionDate = new DateTime(2016, 09, 23),
                Course = coursesToAdd[0],
                Student = studentsToAdd[1]
            });

            context.Homeworks.AddOrUpdate(h => h.SubmissionDate, new Homework()
            {
                Content = "CRUD",
                ContentType = ContentType.Zip,
                SubmissionDate = new DateTime(2016, 09, 25),
                Course = coursesToAdd[0],
                Student = studentsToAdd[2]
            });

            context.Homeworks.AddOrUpdate(h => h.SubmissionDate, new Homework()
            {
                Content = "Introduction to EF",
                ContentType = ContentType.Application,
                SubmissionDate = new DateTime(2016, 10, 23),
                Course = coursesToAdd[1],
                Student = studentsToAdd[3]
            });

            context.Homeworks.AddOrUpdate(h => h.SubmissionDate, new Homework()
            {
                Content = "Introduction to EF",
                ContentType = ContentType.Application,
                SubmissionDate = new DateTime(2016, 10, 21),
                Course = coursesToAdd[1],
                Student = studentsToAdd[4]
            });

            context.Homeworks.AddOrUpdate(h => h.SubmissionDate, new Homework()
            {
                Content = "Introduction to EF",
                ContentType = ContentType.Application,
                SubmissionDate = new DateTime(2016, 10, 22),
                Course = coursesToAdd[1],
                Student = studentsToAdd[0]
            });

            context.Homeworks.AddOrUpdate(h => h.SubmissionDate, new Homework()
            {
                Content = "Java Blog Queries",
                ContentType = ContentType.Pdf,
                SubmissionDate = new DateTime(2016, 07, 22),
                Course = coursesToAdd[2],
                Student = studentsToAdd[1]
            });

            context.Homeworks.AddOrUpdate(h => h.SubmissionDate, new Homework()
            {
                Content = "Java Blog Queries",
                ContentType = ContentType.Pdf,
                SubmissionDate = new DateTime(2016, 07, 23),
                Course = coursesToAdd[2],
                Student = studentsToAdd[2]
            });

            context.Homeworks.AddOrUpdate(h => h.SubmissionDate, new Homework()
            {
                Content = "Java Blog Queries",
                ContentType = ContentType.Pdf,
                SubmissionDate = new DateTime(2016, 07, 24),
                Course = coursesToAdd[2],
                Student = studentsToAdd[4]
            });

            context.Licenses.AddOrUpdate(l => l.Name, new License()
            {
                Name = "Softuni bg license",
                ResourceId = context.Resources.Find(1).Id
            });

            context.Licenses.AddOrUpdate(l => l.Name, new License()
            {
                Name = "Telerik bg license",
                ResourceId = context.Resources.Find(2).Id
            });

            context.Licenses.AddOrUpdate(l => l.Name, new License()
            {
                Name = "Swift academy bg license",
                ResourceId = context.Resources.Find(3).Id
            });

            context.Users.AddOrUpdate(u => u.Username, new User()
            {
                Username = "Kircata98",
                Password = "123456"
            });

            context.Users.AddOrUpdate(u => u.Username, new User()
            {
                Username = "Kircata97",
                Password = "123456"
            });

            context.Users.AddOrUpdate(u => u.Username, new User()
            {
                Username = "Kircata96",
                Password = "123456"
            });

            context.SaveChanges();

            User kircata98 = context.Users.First();
            User kircata97 = context.Users.Find(2);
            User kircata96 = context.Users.Find(3);
            kircata98.Friends.Add(kircata97);
            kircata98.Friends.Add(kircata96);
            kircata97.Friends.Add(kircata98);
            kircata96.Friends.Add(kircata98);

            context.SaveChanges();

            context.Albums.AddOrUpdate(a => a.Name, new Album()
            {
                Name = "Kircata's album",
                BackgroundColor = "Red",
                IsPublic = true,
                Pictures = new HashSet<Picture>()
                {
                    new Picture()
                    {
                        Title = "Kircata's Profile Picture",
                        FilePath = "C:\\Users\\v\\Downloads\\nakov.jpg",
                        Caption = File.ReadAllBytes("C:\\Users\\v\\Downloads\\nakov.jpg")
                    }
                }
            });

            context.SaveChanges();
        }
    }
}
