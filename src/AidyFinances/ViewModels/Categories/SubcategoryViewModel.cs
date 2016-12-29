using System.ComponentModel.DataAnnotations;

namespace AF.ViewModels.Categories
{
    public class SubcategoryViewModel
    {
        public int SubcategoryId { get; set; }
        [Required]
        public string Name { get; set; }
        public int CategoryId { get; set; }
    }
}