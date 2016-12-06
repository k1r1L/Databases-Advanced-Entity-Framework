namespace UniversitySystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Course
    {
        public Course()
        {
            this.Students = new HashSet<Student>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string NameDescription { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public int Credits { get; set; }

        public virtual ICollection<Student> Students { get; set; }

        public virtual Teacher Teacher { get; set; }

        [ForeignKey("Teacher")]
        public int TeacherId { get; set; }
    }
}
