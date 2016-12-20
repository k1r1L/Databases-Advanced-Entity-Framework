namespace WeddingsPlanner.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public abstract class Present
    {
        [Key, ForeignKey("Invitation")]
        public int Id { get; set; }

        public virtual Person Owner { get; set; }

        public virtual Invitation Invitation { get; set; }
    }
}
