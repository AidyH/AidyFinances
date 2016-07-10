using System.ComponentModel.DataAnnotations;

namespace AF.Models
{
    public class Currency
    {
        public int CurrencyId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [StringLength(3)]
        public string Abbreviation { get; set; }
    }
}