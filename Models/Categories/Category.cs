using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AF.Models
{
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoryId { get; set; }

        [Required]
        public string Name { get; set; }

        // Foreign keys
        [Required]
        public string UserId { get; set; }

        // Navigation properties
        public ApplicationUser User { get; set; }
    }
}