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
                        PayeeId = a.PayeeId,
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

        // GET: Edit
        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            // Load the payee that's being edited
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            string userId = await _userManager.GetUserIdAsync(user);
 
            var payee = _context.Payees.SingleOrDefault(a => a.PayeeId == id && a.UserId == userId);

            PayeeViewModel model = new PayeeViewModel() {
                PayeeId = payee.PayeeId,
                Name = payee.Name
            };

            return View(model);
        }

        // POST: Edit
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PayeeViewModel model)
        {
            // If not valid, repopulate the view

            if (!ModelState.IsValid)
            {
                return View("Edit", model);
            }

            // Update database object

            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            string userId = await _userManager.GetUserIdAsync(user);
            
            Payee payee = new Payee();
            payee = _context.Payees.SingleOrDefault(a => a.PayeeId == model.PayeeId && a.UserId == userId);

            if (payee != null)
            {
                payee.UserId = userId;
                payee.Name = model.Name;
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Payees");
        }

        // GET: Delete
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            // Load the payee that's being deleted
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            string userId = await _userManager.GetUserIdAsync(user);
 
            Payee payee = _context.Payees.SingleOrDefault(a => a.PayeeId == id && a.UserId == userId);

            PayeeViewModel model = new PayeeViewModel() {
                PayeeId = payee.PayeeId,
                Name = payee.Name
            };

            return View(model);
        }

        // POST: Delete
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(PayeeViewModel model)
        {
            // Update database object

            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            string userId = await _userManager.GetUserIdAsync(user);
            
            Payee payee = new Payee();
            payee = _context.Payees.SingleOrDefault(a => a.PayeeId == model.PayeeId && a.UserId == userId);

            if (payee != null)
            {
                _context.Payees.Remove(payee);
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Payees");
        }
    }
}