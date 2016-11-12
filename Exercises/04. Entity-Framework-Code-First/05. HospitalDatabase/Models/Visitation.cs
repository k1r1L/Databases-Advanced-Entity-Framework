namespace _05.HospitalDatabase.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Visitation
    {
        [Key]
        public int VisitationId { get; set; }

        public DateTime Time { get; set; }

        [StringLength(500)]
        public string Comments { get; set; }

        public Patient Patient { get; set; }

        public Doctor Doctor { get; set; }
    }
}
