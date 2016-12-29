using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AF.Models
{
    public class Account
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AccountId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public decimal StartBalence { get; set; }

        [Required]
        public decimal Balence { get; set; }

        // Foreign keys
        [Required]
        public string UserId { get; set; }

        [Required]
        public int AccountTypeId { get; set; }

        [Required]
        public int CurrencyId { get; set; }

        // Navigation properties
        public ApplicationUser User { get; set; }

        public AccountType AccountType { get; set; }

        public Currency Currency { get; set; }
    }
}