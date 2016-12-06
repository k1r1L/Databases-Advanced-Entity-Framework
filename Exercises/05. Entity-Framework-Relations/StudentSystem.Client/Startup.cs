namespace StudentSystem.Client
{
    using Data;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    public class Startup
    {
        public static void Main()
        {
            StudentSystemContext context = new StudentSystemContext();
            //AddNewTag(context);

        }

        private static void AddNewTag(StudentSystemContext context)
        {
            string tagName = Console.ReadLine();
            tagName = TagTransformer.Transform(tagName);
            Tag newTag = new Tag()
            {
                Name = tagName,
            };

            context.Tags.Add(newTag);
            context.SaveChanges();
            Console.WriteLine(newTag.Name + " was added to the database");
        }

        private static void Query05(StudentSystemContext context)
        {
            var filteredStudents = context.Students
                .Select(s => new
                {
                    Name = s.Name,
                    NumberOfCourses = s.Courses.Count,
                    TotalPrice = s.Courses.Sum(c => c.Price),
                    AveragePricePerCourse = s.Courses.Average(c => c.Price)
                })
                .OrderByDescending(s => s.TotalPrice)
                .ThenByDescending(s => s.NumberOfCourses)
                .ThenBy(s => s.Name);

            foreach (var student in filteredStudents)
            {
                Console.WriteLine($"STUDENT NAME: {student.Name}");
                Console.WriteLine($"STUDENT NUMBER OF COURSES: {student.NumberOfCourses}");
                Console.WriteLine($"STUDENT TOTAL PRICE (FROM ALL COURSES): {student.TotalPrice}");
                Console.WriteLine($"STUDENT AVERAGE PRICE PER COURSE: {student.AveragePricePerCourse:F2}");
            }
        }

        private static void Query04(StudentSystemContext context)
        {
            Console.WriteLine("Enter date in format dd/MM/yyyy: ");
            DateTime givenDate = DateTime
                .ParseExact(Console.ReadLine(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            IEnumerable<Course> courses = context.Courses;
            var orderedCourses = courses
                .Where(c => c.StartDate.Ticks <= givenDate.Ticks && givenDate.Ticks <= c.EndDate.Ticks)
                .Select(c => new
                {
                    Name = c.Name,
                    StartDate = c.StartDate,
                    EndDate = c.EndDate,
                    CourseDuration = c.EndDate - c.StartDate,
                    NumOfStudentsEnrolled = c.Students.Count
                })
                .OrderByDescending(c => c.NumOfStudentsEnrolled)
                .ThenByDescending(c => c.CourseDuration);
            foreach (var course in orderedCourses)
            {
                Console.WriteLine($"COURSE NAME: {course.Name}");
                Console.WriteLine($"COURSE START DATE: {course.StartDate}");
                Console.WriteLine($"COURSE END DATE: {course.EndDate}");
                Console.WriteLine($"COURSE DURATION (DAYS): {course.CourseDuration.Days}");
                Console.WriteLine($"COURSE NUMBER OF STUDENTS ENROLLED: {course.NumOfStudentsEnrolled}");
                Console.WriteLine();
            }
        }

        private static void Query03(StudentSystemContext context)
        {
            var courses = context.Courses
                            .Where(c => c.Resources.Count > 5)
                            .OrderByDescending(c => c.Resources.Count)
                            .ThenByDescending(c => c.StartDate)
                            .Select(c => new
                            {
                                CourseName = c.Name,
                                ResourcesCount = c.Resources.Count
                            });

            foreach (var course in courses)
            {
                Console.WriteLine(course.CourseName);
                Console.WriteLine(course.ResourcesCount);
            }
        }

        private static void Query02(StudentSystemContext context)
        {
            IEnumerable<Course> courses = context.Courses
                            .OrderBy(c => c.StartDate)
                            .ThenByDescending(c => c.EndDate);

            foreach (Course course in courses)
            {
                Console.WriteLine($"Course name: {course.Name}\nCourse description: {course.Description}");
                foreach (Resource resource in course.Resources)
                {
                    Console.WriteLine($"Resource name: {resource.Name}");
                }
            }
        }

        private static void Query01(StudentSystemContext context)
        {
            IEnumerable<Student> students = context.Students;

            foreach (Student student in students)
            {
                Console.WriteLine($"Student name: {student.Name}");
                foreach (Homework homework in student.Homeworks)
                {
                    Console.WriteLine($"Homework content: {homework.Content}");
                    Console.WriteLine($"Homework content type: {homework.ContentType}");
                }
            }
        }
    }
}
