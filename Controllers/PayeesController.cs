using AF.Models;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using AF.Data;
using AF.ViewModels.Payees;
using Microsoft.AspNetCore.Authorization;

namespace AF.Controllers.Payees
{
    public class PayeesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public PayeesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Index
        [Authorize]
        public async Task<IActionResult> Index()
        {
            // Get list of payees
            List<PayeeViewModel> model = new List<PayeeViewModel>();

            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            string userId = await _userManager.GetUserIdAsync(user);

            model = ( from a in  _context.Payees
                    where a.UserId == userId
                    select new PayeeViewModel() {
                    Name = a.Name
                    }).ToList();

            return View(model);
        }

        // GET: Add
        [Authorize]
        public IActionResult Add()
        {
            var model = new PayeeViewModel();
            return View(model);
        }        

        // POST: Add
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(PayeeViewModel model)
        {
            // If not valid, repopulate the view
            if (!ModelState.IsValid)
            {
                return View("Add", model);
            }

            // Populate the model 
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            var payee = new Payee() {
                Name = model.Name,
                UserId = await _userManager.GetUserIdAsync(user)
            };

            // Add this to the database context
            _context.Payees.Add(payee);

            // Save it to the database
            _context.SaveChanges();

            return RedirectToAction("Index", "Payees");
        }

    }
}