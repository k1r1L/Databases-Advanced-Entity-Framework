namespace PhotographyWorkshops.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using PhotographyWorkshops.Models.Attributes;
    using PhotographyWorkshops.Models.Contracts;

    public class Camera : ICamera
    {
        public Camera()
        {
            this.PrimaryPhotographers = new HashSet<Photographer>();
            this.SecondaryPhotographers = new HashSet<Photographer>();

        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Model { get; set; }
        [Required]
        public string Make { get; set; }

        [Required, MinIso]
        public int MinIso { get; set; }

        public int MaxIso { get; set; }

        public bool IsFullFrame { get; set; }

        public virtual ICollection<Photographer> PrimaryPhotographers { get; set; }

        public virtual ICollection<Photographer> SecondaryPhotographers { get; set; }
    }
}
