using System.ComponentModel.DataAnnotations;

namespace AF.Models
{
    public class AccountType
    {
        public int AccountTypeId { get; set; }

        [Required]
        public string Name { get; set; }
    }
}