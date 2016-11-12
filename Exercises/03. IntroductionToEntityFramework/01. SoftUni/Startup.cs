namespace _01.SoftUni
{
    using Models;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public class Startup
    {
        public static void Main()
        {
            SoftUniContext context = new SoftUniContext();

            //--Problem 03.
            //IEnumerable<Employee> employees = context.Employees;
            //foreach (var employee in employees)
            //{
            //    Console.WriteLine($"{employee.FirstName} {employee.LastName} {employee.MiddleName} {employee.JobTitle} {employee.Salary}");
            //}

            //--Problem 04.
            //string[] employees = context
            //    .Employees
            //    .Where(e => e.Salary > 50000)
            //    .Select(e => e.FirstName).ToArray();
            //foreach (var employeeFirstName in employees)
            //{
            //    Console.WriteLine(employeeFirstName);
            //}

            //Problem 05.
            //var employees = context.Employees
            //    .Where(e => e.Department.Name == "Research and Development")
            //    .OrderBy(e => e.Salary)
            //    .ThenByDescending(e => e.FirstName);
            //foreach (var employee in employees)
            //{
            //    Console.WriteLine($"{employee.FirstName} {employee.LastName} from {employee.Department.Name} - ${employee.Salary:F2}");
            //}

            //Problem 06.
            //Address address = new Address()
            //{
            //    AddressText = "Vitoshka 15",
            //    TownID = 4
            //};
            //Employee employee = context.Employees
            //    .FirstOrDefault(emp => emp.LastName == "Nakov");
            //employee.Address = address;
            //context.SaveChanges();
            //string[] employeesAddresses = context.Employees
            //    .OrderByDescending(emp => emp.AddressID)
            //    .Select(emp => emp.Address.AddressText)
            //    .Take(10)
            //    .ToArray();
            //foreach (var a in employeesAddresses)
            //{
            //    Console.WriteLine(a);
            //}

            //Problem 07.
            //Project projectToDelete = context.Projects.Find(2);
            //IEnumerable<Employee> employeesWithProject = context.Projects.Find(2).Employees;
            //projectToDelete.Employees.Clear();
            //context.Projects.Remove(projectToDelete);
            //context.SaveChanges();
            //string[] projectsToPrint = context.Projects
            //    .Select(p => p.Name)
            //    .Take(10)
            //    .ToArray();
            //foreach (var project in projectsToPrint)
            //{
            //    Console.WriteLine(project);
            //}

            //Problem 08.
            //var employees = context.Employees
            //    .Where(e => e.Projects.Count(p => p.StartDate.Year >= 2001 && p.StartDate.Year <= 2003) > 0)
            //    .Take(30);
            //foreach (var employee in employees)
            //{
            //    Console.WriteLine($"{employee.FirstName} {employee.LastName} {employee.Manager.FirstName}");
            //    foreach (var project in employee.Projects)
            //    {
            //        Console.WriteLine($"--{project.Name} {project.StartDate} {project.EndDate}");
            //    }
            //}

            //Problem 09.
            //IEnumerable<Address> addresses = context.Addresses
            //    .OrderByDescending(a => a.Employees.Count)
            //    .ThenBy(a => a.Town.Name)
            //    .Take(10);
            //foreach (var address in addresses)
            //{
            //    Console.WriteLine($"{address.AddressText}, {address.Town.Name} – {address.Employees.Count} employees");
            //}

            //Problem 10.
            //Employee employeeWithId147 = context.Employees.Find(147);
            //Console.WriteLine($"{employeeWithId147.FirstName} {employeeWithId147.LastName} {employeeWithId147.JobTitle}");
            //string[] projectNames = employeeWithId147.Projects
            //    .OrderBy(p => p.Name)
            //    .Select(p => p.Name)
            //    .ToArray();
            //foreach (var name in projectNames)
            //{
            //    Console.WriteLine($"{name}");
            //}

            //Problem 11.
            //IEnumerable<Department> departments = context.Departments
            //    .Where(d => d.Employees.Count > 5)
            //    .OrderBy(d => d.Employees.Count);
            //foreach (var department in departments)
            //{
            //    Console.WriteLine($"{department.Name} {department.Manager.FirstName}");
            //    foreach (var employee in department.Employees)
            //    {
            //        Console.WriteLine($"{employee.FirstName} {employee.LastName} {employee.JobTitle}");
            //    }
            //}

            //Problem 12.
            //Stopwatch timer = new Stopwatch();
            //timer.Start();
            //PrintNamesWithNativeSqlQuery(context);
            //timer.Stop();
            //Console.WriteLine(timer.Elapsed);

            //timer.Start();
            //PrintNamesWithLinq(context);
            //timer.Stop();
            //Console.WriteLine(timer.Elapsed);

            //Problem 15.
            //IEnumerable<Project> last10Projects = context.Projects
            //    .OrderByDescending(p => p.StartDate)
            //    .Take(10)
            //    .OrderBy(p => p.Name);
            //foreach (var project in last10Projects)
            //{
            //    Console.WriteLine($"{project.Name} {project.Description} {project.StartDate} {project.EndDate}");
            //}

            //Problem 16.
            //IEnumerable<Employee> employeesToUpdate = context.Employees
            //    .Where(e => e.DepartmentID == 1 || e.DepartmentID == 2 || e.DepartmentID == 4 || e.DepartmentID == 11);
            //foreach (var emp in employeesToUpdate)
            //{
            //    emp.Salary *= 1.12m;
            //    Console.WriteLine($"{emp.FirstName} {emp.LastName} (${emp.Salary})");
            //}

            //context.SaveChanges();

            //Problem 17.
            //string townName = Console.ReadLine();
            //Town townToDelete = context.Towns.First(t => t.Name == townName);
            //IEnumerable<Address> addressesToDelete = townToDelete.Addresses;
            //int addressesCount = addressesToDelete.Count();
            //foreach (var emp in context.Employees)
            //{
            //    if (addressesToDelete.Contains(emp.Address))
            //    {
            //        emp.AddressID = null;
            //    }
            //}

            //context.Addresses.RemoveRange(addressesToDelete);
            //context.Towns.Remove(townToDelete);
            //context.SaveChanges();
            //Console.WriteLine($"{addressesCount} addresses in {townToDelete.Name} were deleted");

            //Problem 18.
            //IEnumerable<Employee> employees = context.Employees
            //    .Where(emp => emp.FirstName.Substring(0, 2) == "SA");
            //foreach (var emp in employees)
            //{
            //    Console.WriteLine($"{emp.FirstName} {emp.LastName} – {emp.JobTitle} - (${emp.Salary})");
            //}
            
        }

        private static void PrintNamesWithLinq(SoftUniContext context)
        {
            string[] employeeNames = context.Employees
                .Where(e => e.Projects.Count(p => p.StartDate.Year == 2002) > 0)
                .Select(e => e.FirstName)
                .ToArray();

            foreach (var employeeName in employeeNames)
            {
                Console.WriteLine(employeeName);
            }
        }

        private static void PrintNamesWithNativeSqlQuery(SoftUniContext context)
        {
            string sqlQuery = "SELECT e.FirstName FROM Employees AS e " +
                "INNER JOIN EmployeesProjects AS ep ON ep.EmployeeID = e.EmployeeID " +
                "INNER JOIN Projects AS p ON ep.ProjectID = p.ProjectID " +
                "WHERE YEAR(p.StartDate) = 2002 " +
                "GROUP BY e.FirstName";

            string[] employeeNames = context.Database.SqlQuery<string>(sqlQuery).ToArray();
            foreach (var employeeName in employeeNames)
            {
                Console.WriteLine(employeeName);
            }
        }
    }
}
