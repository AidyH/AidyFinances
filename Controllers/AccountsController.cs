using AF.Models;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Threading.Tasks;
using AF.ViewModels;
using AF.Data;
using Microsoft.AspNetCore.Authorization;

namespace AF.Controllers.Accounts
{
    public class AccountsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Add
        [Authorize]
        public ActionResult Add()
        {
            // Fill the drop down lists

            var viewModel = new AccountsAddViewModel();
            viewModel.AccountTypes = _context.AccountTypes.ToList();
            viewModel.Currencies = _context.Currencies.ToList();

            return View(viewModel);
        }
        
        
        //public ActionResult Add(AccountsAddViewModel viewModel)

        // POST: Add
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(AccountsAddViewModel viewModel)
        {
            // If not valid, repopulate the view

            if (!ModelState.IsValid)
            {
                viewModel.AccountTypes = _context.AccountTypes.ToList();
                viewModel.Currencies = _context.Currencies.ToList();
                return View("Add", viewModel);
            }

            // Populate the model

            var account = new Account();
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);

            account.UserId = await _userManager.GetUserIdAsync(user);
            account.Name = viewModel.Name;
            account.StartBalence = viewModel.StartBalence;
            account.Balence = viewModel.Balence;
            account.AccountTypeId = viewModel.AccountType;
            account.CurrencyId = viewModel.Currency;

            // Add this to the database context
            _context.Accounts.Add(account);

            // Save it to the database
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
    }
}