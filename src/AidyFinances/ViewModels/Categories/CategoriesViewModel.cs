using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using AF.Models;

namespace AF.ViewModels.Categories
{
    public class CategoryViewModel
    {
        public int CategoryId { get; set; }
        
        [Required]
        public string Name { get; set; }

        public IEnumerable<Subcategory> Subcategories { get; set; }
    }
}