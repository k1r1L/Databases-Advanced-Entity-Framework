namespace _01.GringottsDatabase.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    
    public class WizardDeposit
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(Constants.FirstNameStringLength)]
        public string FirstName { get; set; }

        [MaxLength(Constants.LastNameStringLength)]
        public string LastName { get; set; }

        [MaxLength(Constants.NotesStringLength)]
        public string Notes { get; set; }

        [Required]
        public int Age { get; set; }

        [MaxLength(Constants.MagicWandCreatorStringLength)]
        public string MagicWandCreator { get; set; }

        [Range(Constants.MagicWandMinSize, Constants.MagicWandMaxSize)]
        public int MagicWandSize { get; set; }

        [MaxLength(Constants.DepositGroupStringLength)]
        public string DepositGroup { get; set; }

        public DateTime DepositStartDate { get; set; }

        public decimal DepositAmount { get; set; }

        public decimal DepositInterest { get; set; }

        public decimal DepositCharge { get; set; }

        public DateTime DepositExpirationDate { get; set; }

        public bool IsDepositExpired { get; set; }
    }
}
