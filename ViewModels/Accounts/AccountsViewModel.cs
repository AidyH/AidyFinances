using System.ComponentModel.DataAnnotations;

namespace AF.ViewModels.Accounts
{
    public class AccountsViewModel
    {
        public int AccountId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Starting Balance")]
        public decimal StartBalence { get; set; }

        [Required]
        public decimal Balence { get; set; }

        [Required]
        public string Currency { get; set; }

        [Required]
        [Display(Name = "Account Type")]
        public string AccountType { get; set; }
    }
}