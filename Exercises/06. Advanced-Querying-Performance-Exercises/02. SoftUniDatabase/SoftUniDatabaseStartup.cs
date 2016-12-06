namespace _02.SoftUniDatabase
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    public class SoftUniDatabaseStartup
    {
        public static void Main(string[] args)
        {
            SoftUniContext context = new SoftUniContext();

            // Task 04.
            //BeforeCallingToList(context);
            //AfterCallingToList(context);
        }

        private static void AfterCallingToList(SoftUniContext context)
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();
            IEnumerable<Employee> employees = context.Employees
                .ToList()
                .OrderBy(e => e.JobTitle)
                .ThenByDescending(e => e.DepartmentID);
            timer.Stop();

            Console.WriteLine($"After ToList(): {timer.Elapsed}");
        }

        private static void BeforeCallingToList(SoftUniContext context)
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();
            IEnumerable<Employee> employees = context.Employees
                .OrderBy(e => e.JobTitle)
                .ThenByDescending(e => e.DepartmentID)
                .ToList();
            timer.Stop();

            Console.WriteLine($"Before ToList(): {timer.Elapsed}");
        }
    }
}
