namespace _02.SoftuniDatabase
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Startup
    {
        public static void Main(string[] args)
        {
            SoftuniContext context = new SoftuniContext();
            //17. CallStoredProcedure(context);
            //18. EmployeesMaximumSalary(context);
        }

        private static void EmployeesMaximumSalary(SoftuniContext context)
        {
            context.Departments
                .Where(d => d.Employee.Salary < 30000 || d.Employee.Salary > 70000)
                .GroupBy(department => new
                {
                    DepartmentName = department.Name,
                    DepartmentMaxSalary = department.Employees.Max(e => e.Salary)
                })
                .ToList()
                .ForEach(department => Console.WriteLine($"{department.Key.DepartmentName} - {department.Key.DepartmentMaxSalary:F2}"));
        }

        private static void CallStoredProcedure(SoftuniContext context)
        {
            Console.WriteLine("Enter employee first name: ");
            string firstName = Console.ReadLine();
            Console.WriteLine("Enter employee last name: ");
            string lastName = Console.ReadLine();
            var projects = context.GetProjectsByEmployee(firstName, lastName);
            foreach (var p in projects)
            {
                Console.WriteLine($"{p.Name} - {p.Description} - {p.StartDate}");
            }
        }
    }
}
