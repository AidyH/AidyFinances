using System.ComponentModel.DataAnnotations;

namespace AF.ViewModels.Payees
{
    public class PayeeViewModel
    {
        public int PayeeId { get; set; }
        
        [Required]
        public string Name { get; set; }
    }
}