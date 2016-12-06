namespace StudentSystem.Models
{
    using Attributes;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Tag
    {
        public Tag()
        {
            this.Albums = new HashSet<Album>();
        }

        [Key, Tag]
        public string Name { get; set; }

        public virtual ICollection<Album> Albums { get; set; }
    }
}
