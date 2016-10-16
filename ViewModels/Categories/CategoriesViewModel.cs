using System.ComponentModel.DataAnnotations;

namespace AF.ViewModels.Categories
{
    public class CategoryViewModel
    {
        public int CategoryId { get; set; }
        
        [Required]
        public string Name { get; set; }
    }
}