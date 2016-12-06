namespace UniversitySystem.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Teacher : Person
    {
        public Teacher()
        {
            this.Courses = new HashSet<Course>();
        }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public decimal SalaryPerHour { get; set; }

        public virtual ICollection<Course> Courses { get; set; }
    }
}
