namespace _05.HospitalDatabase.Models
{
    using System.Collections;
    using System.ComponentModel.DataAnnotations;

    public class Diagnose
    {
        [Key]
        public int DiagnoseId { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Comments { get; set; }

        public Patient Patient { get; set; }
    }
}
