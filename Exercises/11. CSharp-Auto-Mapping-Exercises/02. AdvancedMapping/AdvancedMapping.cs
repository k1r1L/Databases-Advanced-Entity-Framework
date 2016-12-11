namespace _02.AdvancedMapping
{
    using AutoMapper;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    
    public class AdvancedMapping
    {
        public static void Main()
        {
            ConfigureAutoMapper();
            EmployeeContext context = new EmployeeContext();
            List<ManagerDto> managerDtos = new List<ManagerDto>();
            foreach (var emp in context.Employees)
            {
                managerDtos.Add(Mapper.Map<ManagerDto>(emp));
            }

            foreach (ManagerDto manager in managerDtos)
            {
                Console.WriteLine("-MANAGER:");
                Console.WriteLine(manager.FirstName + " " + manager.LastName);
                Console.WriteLine("--EMPLOYEES:");
                foreach (EmployeeDto employee in manager.Employees)
                {
                    Console.WriteLine("EMPLOYEE NAME: {0}", employee.FirstName + " " + employee.LastName);
                    Console.WriteLine("EMPLOYEE SALARY: {0}", employee.Salary);
                    Console.WriteLine("EMPLOYEE MANAGER NAME: {0}", employee.ManagerName);
                }

                if (manager.Employees.Count == 0)
                {
                    Console.Write("NONE");
                    Console.WriteLine();
                }
            }
        }

        private static void SeedDatabase(EmployeeContext context)
        {
            context.Employees.Add(new Employee()
            {
                FirstName = "Kiril",
                LastName = "Kirilov",
                IsOnHoliday = true,
                Salary = 10000m,
                Birthday = new DateTime(1998, 01, 06)
            });

            context.SaveChanges();
            Employee kiro = context.Employees.First();
            context.Employees.Add(new Employee()
            {
                FirstName = "Ivaylo",
                LastName = "Jelev",
                IsOnHoliday = false,
                Salary = 100m,
                Birthday = new DateTime(1995, 06, 21),
                Manager = kiro
            });

            context.Employees.Add(new Employee()
            {
                FirstName = "Nqkuv",
                LastName = "Nekoisi",
                IsOnHoliday = true,
                Salary = 555m,
                Birthday = new DateTime(1999, 01, 06),
                Manager = kiro
            });

            context.Employees.Add(new Employee()
            {
                FirstName = "Petar",
                LastName = "Petrov",
                IsOnHoliday = false,
                Salary = 665m,
                Birthday = new DateTime(1996, 01, 06),
                Manager = kiro

            });

            context.Employees.Add(new Employee()
            {
                FirstName = "Ivan",
                LastName = "Stamboliev",
                IsOnHoliday = false,
                Salary = 10000m,
                Birthday = new DateTime(1996, 01, 06),
                Manager = kiro
            });
        }

        private static void ConfigureAutoMapper()
        {
            Mapper.Initialize(expression =>
            {
                expression.CreateMap<Employee, EmployeeDto>().ForMember(
                    empDto => empDto.ManagerName,
                    configExpression => configExpression.MapFrom(emp => emp.Manager.FirstName));
                expression.CreateMap<Employee, ManagerDto>();
            });
        }
    }
}
