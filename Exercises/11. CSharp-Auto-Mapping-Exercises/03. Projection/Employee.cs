namespace _03.Projection
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(25)]
        public string FirstName { get; set; }

        [Required, StringLength(25)]
        public string LastName { get; set; }

        [Required]
        public decimal Salary { get; set; }

        [Required]
        public DateTime Birthday { get; set; }

        public string Address { get; set; }

        public int? ManagerId { get; set; }

        [ForeignKey("ManagerId")]
        public Employee Manager { get; set; }
    }
}
