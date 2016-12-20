namespace WeddingsPlanner.Models
{
    using Attributes;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Invitation
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public virtual Wedding Wedding { get; set; }

        [Required]
        public virtual Person Guest { get; set; }

        public virtual Present Present { get; set; }

        public bool IsAttending { get; set; }

        [Family]
        public string Family { get; set; }
    }
}
