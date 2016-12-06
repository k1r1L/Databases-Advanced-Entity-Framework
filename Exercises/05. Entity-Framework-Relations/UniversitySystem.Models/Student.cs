namespace UniversitySystem.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Student : Person
    {
        public Student()
        {
            this.Courses = new HashSet<Course>();
        }

        [Required, Range(2, 6)]
        public decimal AverageGrade { get; set; }

        [Required]
        public Attendance Attendance { get; set; }

        public virtual ICollection<Course> Courses { get; set; }
    }
}
