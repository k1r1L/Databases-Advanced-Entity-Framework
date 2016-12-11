namespace PhotographyWorkshops.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;


    public class Workshop
    {
        public Workshop()
        {
            this.Participants = new HashSet<Photographer>();
        }

        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        public decimal PricePerParticipant { get; set; }

        public virtual Photographer Trainer { get; set; }

        public ICollection<Photographer> Participants { get; set; }
    }
}
