namespace PhotographyWorkshops.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Photographer
    {
        public Photographer()
        {
            this.Lenses = new HashSet<Lens>();
            this.Accessories = new HashSet<Accessory>();
            this.TrainerWorkshops = new HashSet<Workshop>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required, MinLength(2), MaxLength(50)]
        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public virtual Camera PrimaryCamera { get; set; }

        public virtual Camera SecondaryCamera { get; set; }

        public virtual ICollection<Lens> Lenses { get; set; }

        public virtual ICollection<Accessory> Accessories { get; set; }

        public virtual ICollection<Workshop> TrainerWorkshops { get; set; }

        public virtual ICollection<Workshop> ParicipantWorkshops { get; set; }

    }
}
