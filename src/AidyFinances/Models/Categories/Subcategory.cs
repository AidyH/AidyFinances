using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AF.Models
{
    public class Subcategory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SubcategoryId { get; set; }

        [Required]
        public string Name { get; set; }

        // Foreign keys
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public string UserId { get; set; }

        // Navigation properties
        public Category Category { get; set; }
        public ApplicationUser User { get; set; }
    }
}