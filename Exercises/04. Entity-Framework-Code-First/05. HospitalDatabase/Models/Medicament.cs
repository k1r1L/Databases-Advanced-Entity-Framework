namespace _05.HospitalDatabase.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.ComponentModel.DataAnnotations;

    public class Medicament
    {
        [Key]
        public int MedicamentId { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        public Patient Patient { get; set; }
    }
}
