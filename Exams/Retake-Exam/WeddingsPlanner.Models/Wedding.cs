namespace WeddingsPlanner.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Wedding
    {
        public Wedding()
        {
            this.Invitations = new HashSet<Invitation>();
            this.Venues = new HashSet<Venue>();
        }

        [Key]
        public int Id { get; set; }

        public DateTime Date { get; set; }

        [Required]
        public virtual Person Bride { get; set; }

        [Required]
        public virtual Person Bridegroom { get; set; }

        public virtual Agency Agency { get; set; }

        public virtual ICollection<Invitation> Invitations { get; set; }

        public virtual ICollection<Venue> Venues { get; set; }
    }
}
