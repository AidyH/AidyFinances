using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AF.Models;

namespace AF.ViewModels
{
    public class AccountsAddViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Starting Balance")]
        public decimal StartBalence { get; set; }

        [Required]
        public decimal Balence { get; set; }

        [Required]
        public int Currency { get; set; }

        public IEnumerable<Currency> Currencies { get; set; }

        [Required]
        [Display(Name = "Account Type")]
        public int AccountType { get; set; }

        public IEnumerable<AccountType> AccountTypes { get; set; }
    }
}