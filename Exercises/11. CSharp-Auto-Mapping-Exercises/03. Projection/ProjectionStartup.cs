namespace _03.Projection
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using System;
    using System.Linq;
    
    public class ProjectionStartup
    {
        public static void Main()
        {
            ConfigureMapping();
            ProjectionContext context = new ProjectionContext();
            //SeedDatabase(context);
            var employeeDtos = context.Employees
                .Where(e => e.Birthday.Year < 1990)
                .OrderByDescending(e => e.Salary)
                .ProjectTo<EmployeeDto>()
                .ToList();
            foreach (EmployeeDto empDto in employeeDtos)
            {
                string managerName = empDto.ManagerLastName != null ? empDto.ManagerLastName : "[no manager]";
                Console.WriteLine($"{empDto.FirstName} {empDto.LastName} {empDto.Salary} – Manager: {managerName}");
            }
        }

        private static void SeedDatabase(ProjectionContext context)
        {
            context.Employees.Add(new Employee()
            {
                FirstName = "Steve",
                LastName = "Jobbsen",
                Salary = 6000.20m,
                Birthday = new DateTime(1989, 12, 12),
                Address = "Nqkude si"
            });

            context.SaveChanges();
            Employee manager = context.Employees.First();
            context.Employees.Add(new Employee()
            {
                FirstName = "Kirilyc",
                LastName = "Lefi",
                Salary = 4400.00m,
                Birthday = new DateTime(1977, 12, 12),
                Address = "Nqkude si ulica 2",
                Manager = manager
            });

            context.Employees.Add(new Employee()
            {
                FirstName = "Stephen",
                LastName = "Bjorn",
                Salary = 4300.00m,
                Birthday = new DateTime(1987, 12, 12),
                Address = "Nqkude si ulica 3",
                Manager = manager
            });

            context.Employees.Add(new Employee()
            {
                FirstName = "Kiril",
                LastName = "Kirilov",
                Salary = 1200m,
                Birthday = new DateTime(1998, 01, 06),
                Address = "Sofia, j.k Suha Reka",
            });

            context.Employees.Add(new Employee()
            {
                FirstName = "Alex",
                LastName = "Mihov",
                Salary = 1000m,
                Birthday = new DateTime(1996, 01, 01),
                Address = "Nqkude si ulica 4",
                Manager = manager
            });
            context.SaveChanges();
        }

        private static void ConfigureMapping()
        {
            Mapper.Initialize(expression =>
            {
                expression.CreateMap<Employee, EmployeeDto>()
                .ForMember(empDto => empDto.ManagerLastName, configurationExpression => 
                     configurationExpression.MapFrom(emp => emp.Manager.LastName)
                );
            });
        }
    }
}
