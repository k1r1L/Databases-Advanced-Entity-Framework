namespace StudentSystem.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.IO;

    public class Picture
    {
        private byte[] caption;

        public Picture()
        {
            this.Albums = new HashSet<Album>();
        }

        [Key]
        public int Id { get; set; }

        [Required, StringLength(50)]
        public string Title { get; set; }

        public byte[] Caption
        {
            get { return this.caption; }
            set { this.caption = File.ReadAllBytes(this.FilePath); }
        }

        [Required]
        public string FilePath { get; set; }

        public virtual ICollection<Album> Albums { get; set; }
    }
}
