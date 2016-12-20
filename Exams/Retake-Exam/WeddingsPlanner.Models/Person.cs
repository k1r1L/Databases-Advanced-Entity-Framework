namespace WeddingsPlanner.Models
{
    using Attributes;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Person
    {
        public Person()
        {
            this.Invitations = new HashSet<Invitation>();
        }

        [Key]
        public int Id { get; set; }

        [Required, MinLength(1), MaxLength(60)]
        public string FirstName { get; set; }

        [Required, StringLength(1)]
        public string MiddleNameInitial { get; set; }

        [Required, MinLength(2)]
        public string LastName { get; set; }

        [NotMapped]
        public string FullName
        {
            get { return $"{this.FirstName} {this.MiddleNameInitial} {this.LastName}"; }
        }

        [Required, Gender]
        public string Gender { get; set; }

        public DateTime? Birthdate { get; set; }

        [NotMapped]
        public int Age
        {
            get { return CalculateAge(); }
        }

        public string Phone { get; set; }

        [Email]
        public string Email { get; set; }


        public virtual ICollection<Invitation> Invitations { get; set; }

        [InverseProperty("Bride")]
        public virtual ICollection<Wedding> BrideWeddings { get; set; }

        [InverseProperty("Bridegroom")]
        public virtual ICollection<Wedding> BrideGroomWeddings { get; set; }

        private int CalculateAge()
        {
            if (this.Birthdate != null)
            {
                int currentYear = DateTime.Now.Year;
                int birthDayYear = this.Birthdate.Value.Year;

                return currentYear - birthDayYear;
            }

            return 0;
        }
    }
}
