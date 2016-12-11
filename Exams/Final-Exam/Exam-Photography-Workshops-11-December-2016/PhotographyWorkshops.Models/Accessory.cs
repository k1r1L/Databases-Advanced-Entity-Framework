namespace PhotographyWorkshops.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Accessory
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual Photographer Owner { get; set; }
    }
}
