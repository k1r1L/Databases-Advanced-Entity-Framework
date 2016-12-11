namespace _01.SimpleMapping
{
    using AutoMapper;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class SimpleMapping
    {
        public static void Main(string[] args)
        {
            ConfigureAutoMapper();
            EmployeeContext context = new EmployeeContext();
            Employee kiril = context.Employees.First();
            EmployeeDto kirilDto = Mapper.Map<EmployeeDto>(kiril);
        }

        private static void ConfigureAutoMapper()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Employee, EmployeeDto>());
        }
    }
}
