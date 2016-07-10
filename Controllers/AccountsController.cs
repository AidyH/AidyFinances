using AF.Models;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using AF.ViewModels;
using AF.Data;
using Microsoft.AspNetCore.Authorization;

namespace AF.Controllers.Accounts
{
    public class AccountsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Add
        [Authorize]
        public ActionResult Add()
        {
            // Fill the drop down lists

            var viewModel = new AccountsAddViewModel
            {
                AccountTypes = _context.AccountTypes.ToList(),
                Currencies = _context.Currencies.ToList()
            };


            return View(viewModel);
        }


        // POST: Add
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Add(AccountsAddViewModel viewModel)
        {
            // If not valid, repopulate the view

            if (!ModelState.IsValid)
            {
                viewModel.AccountTypes = _context.AccountTypes.ToList();
                viewModel.Currencies = _context.Currencies.ToList();
                return View("Add", viewModel);
            }

            // Populate the model
            var account = new Account
            {
                UserId = _userManager.GetUserId(HttpContext.User),
                Name = viewModel.Name,
                StartBalence = viewModel.StartBalence,
                Balence = viewModel.Balence,
                AccountTypeId = viewModel.AccountType,
                CurrencyId = viewModel.Currency
            };

            // Add this to the database context
            _context.Accounts.Add(account);

            // Save it to the database
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
    }
}