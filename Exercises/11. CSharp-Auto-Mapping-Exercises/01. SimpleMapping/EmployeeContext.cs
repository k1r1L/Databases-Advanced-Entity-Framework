namespace _01.SimpleMapping
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class EmployeeContext : DbContext
    {
        public EmployeeContext()
            : base("name=EmployeeContext")
        {
        }

        public IDbSet<Employee> Employees { get; set; }

    }
}