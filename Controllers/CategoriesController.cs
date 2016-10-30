using AF.Models;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using AF.Data;
using AF.ViewModels.Categories;
using Microsoft.AspNetCore.Authorization;

namespace AF.Controllers.Categories
{
    public class CategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CategoriesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Index
        [Authorize]
        public async Task<IActionResult> Index()
        {
            // Get list of categories
            List<CategoryViewModel> model = new List<CategoryViewModel>();

            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            string userId = await _userManager.GetUserIdAsync(user);

            model = ( from c in  _context.Categories
                    where c.UserId == userId
                    select new CategoryViewModel() {
                        CategoryId = c.CategoryId,
                        Name = c.Name,
                        Subcategories = from s in _context.Subcategories 
                                        where s.CategoryId == c.CategoryId 
                                        select new Subcategory(){
                                            SubcategoryId = s.SubcategoryId,
                                            Name = s.Name,
                                            CategoryId = s.CategoryId
                                        }
                    }).ToList();

            return View(model);
        }

        // GET: Add
        [Authorize]
        public IActionResult Add()
        {
            var model = new CategoryViewModel();
            return View(model);
        }        

        // POST: Add
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(CategoryViewModel model)
        {
            // If not valid, repopulate the view
            if (!ModelState.IsValid)
            {
                return View("Add", model);
            }

            // Populate the model 
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            var category = new Category() {
                Name = model.Name,
                UserId = await _userManager.GetUserIdAsync(user)
            };

            // Add this to the database context
            _context.Categories.Add(category);

            // Save it to the database
            _context.SaveChanges();

            return RedirectToAction("Index", "Categories");
        }

        // GET: AddSubcategory
        [Authorize]
        public IActionResult AddSubcategory(int id)
        {
            var model = new SubcategoryViewModel() {
                CategoryId = id
            };
            return View(model);
        }

        // POST: AddSubcategory
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddSubcategory(SubcategoryViewModel model)
        {
            // If not valid, repopulate the view
            if (!ModelState.IsValid)
            {
                return View("AddSubcategory", model);
            }

            // Populate the model 
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            var subcategory = new Subcategory() {
                Name = model.Name,
                CategoryId = model.CategoryId,
                UserId = await _userManager.GetUserIdAsync(user)
            };

            // Add this to the database context
            _context.Subcategories.Add(subcategory);

            // Save it to the database
            _context.SaveChanges();

            return RedirectToAction("Index", "Categories");
        }     

        // GET: Edit
        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            // Load the category that's being edited
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            string userId = await _userManager.GetUserIdAsync(user);
 
            var category = _context.Categories.SingleOrDefault(a => a.CategoryId == id && a.UserId == userId);

            CategoryViewModel model = new CategoryViewModel() {
                CategoryId = category.CategoryId,
                Name = category.Name
            };

            return View(model);
        }

        // POST: Edit
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CategoryViewModel model)
        {
            // If not valid, repopulate the view

            if (!ModelState.IsValid)
            {
                return View("Edit", model);
            }

            // Update database object

            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            string userId = await _userManager.GetUserIdAsync(user);
            
            Category category = new Category();
            category = _context.Categories.SingleOrDefault(a => a.CategoryId == model.CategoryId && a.UserId == userId);

            if (category != null)
            {
                category.UserId = userId;
                category.Name = model.Name;
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Categories");
        }

        // GET: EditSubcategory
        [Authorize]
        public async Task<IActionResult> EditSubcategory(int id)
        {
            // Load the subcategory that's being edited
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            string userId = await _userManager.GetUserIdAsync(user);
 
            Subcategory subcategory = new Subcategory();
            subcategory = _context.Subcategories.SingleOrDefault(s => s.SubcategoryId == id && s.UserId == userId);

            SubcategoryViewModel model = new SubcategoryViewModel() {
                SubcategoryId = subcategory.SubcategoryId,
                Name = subcategory.Name
            };

            return View(model);
        }

        // POST: EditSubcategory
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditSubcategory(SubcategoryViewModel model)
        {
            // If not valid, repopulate the view

            if (!ModelState.IsValid)
            {
                return View("EditSubcategory", model);
            }

            // Update database object

            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            string userId = await _userManager.GetUserIdAsync(user);
            
            Subcategory subcategory = new Subcategory();
            subcategory = _context.Subcategories.SingleOrDefault(s => s.SubcategoryId == model.SubcategoryId && s.UserId == userId);

            if (subcategory != null)
            {
                subcategory.UserId = userId;
                subcategory.Name = model.Name;
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Categories");
        }

        // GET: Delete
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            // Load the category that's being deleted
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            string userId = await _userManager.GetUserIdAsync(user);
 
            Category category = _context.Categories.SingleOrDefault(a => a.CategoryId == id && a.UserId == userId);

            CategoryViewModel model = new CategoryViewModel() {
                CategoryId = category.CategoryId,
                Name = category.Name
            };

            return View(model);
        }

        // POST: Delete
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(CategoryViewModel model)
        {
            // Update database object

            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            string userId = await _userManager.GetUserIdAsync(user);
            
            Category category = new Category();
            category = _context.Categories.SingleOrDefault(a => a.CategoryId == model.CategoryId && a.UserId == userId);

            if (category != null)
            {
                _context.Categories.Remove(category);
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Categories");
        }

        // GET: DeleteSubcategory
        [Authorize]
        public async Task<IActionResult> DeleteSubcategory(int id)
        {
            // Load the subcategory that's being deleted
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            string userId = await _userManager.GetUserIdAsync(user);
 
            Subcategory subcategory = _context.Subcategories.SingleOrDefault(s => s.SubcategoryId == id && s.UserId == userId);

            SubcategoryViewModel model = new SubcategoryViewModel() {
                SubcategoryId = subcategory.SubcategoryId,
                Name = subcategory.Name
            };

            return View(model);
        }

        // POST: DeleteSubcategory
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteSubcategory(SubcategoryViewModel model)
        {
            // Update database object

            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            string userId = await _userManager.GetUserIdAsync(user);
            
            Subcategory subcategory = new Subcategory();
            subcategory = _context.Subcategories.SingleOrDefault(a => a.SubcategoryId == model.SubcategoryId && a.UserId == userId);

            if (subcategory != null)
            {
                _context.Subcategories.Remove(subcategory);
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Categories");
        }
    }
}