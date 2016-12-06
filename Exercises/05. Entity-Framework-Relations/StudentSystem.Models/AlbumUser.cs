namespace StudentSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class AlbumUser
    {
        [Key]
        [Column(Order = 0)]
        public int AlbumId { get; set; }

        [Key]
        [Column(Order = 1)]
        public int UserId { get; set; }

        public virtual Album Album { get; set; }

        public virtual User User { get; set; }

        public Role Role { get; set; }
    }
}
