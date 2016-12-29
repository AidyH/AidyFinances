using AF.Models;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using AF.ViewModels.Accounts;
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

        // GET: Index
        [Authorize]
        public IActionResult Index()
        {
            // Get list of accounts
            List<AccountsViewModel> viewModel = new List<AccountsViewModel>();
            viewModel = ( from a in  _context.Accounts
                        select new AccountsViewModel() {
                            AccountId = a.AccountId,
                            Name = a.Name,
                            Balence = a.Balence,
                            StartBalence = a.StartBalence,
                            Currency = a.Currency.Abbreviation,
                            AccountType = a.AccountType.Name
                        }).ToList();

            return View(viewModel);
        }

        // GET: Add
        [Authorize]
        public IActionResult Add()
        {
            // Fill the drop down lists

            var viewModel = new AccountViewModel();
            viewModel.AccountTypes = _context.AccountTypes.ToList();
            viewModel.Currencies = _context.Currencies.ToList();

            return View(viewModel);
        }
        

        // POST: Add
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(AccountViewModel viewModel)
        {
            // If not valid, repopulate the view

            if (!ModelState.IsValid)
            {
                viewModel.AccountTypes = _context.AccountTypes.ToList();
                viewModel.Currencies = _context.Currencies.ToList();
                return View("Add", viewModel);
            }

            // Populate the model 
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            var account = new Account() {
                UserId = await _userManager.GetUserIdAsync(user),
                Name = viewModel.Name,
                StartBalence = viewModel.StartBalence,
                Balence = viewModel.Balence,
                AccountTypeId = viewModel.AccountType,
                CurrencyId = viewModel.Currency,
            };

            // Add this to the database context
            _context.Accounts.Add(account);

            // Save it to the database
            _context.SaveChanges();

            return RedirectToAction("Index", "Accounts");
        }

        // GET: Edit
        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            // Load the account that's being edited
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            string userId = await _userManager.GetUserIdAsync(user);
 
            var account = _context.Accounts.SingleOrDefault(a => a.AccountId == id && a.UserId == userId);

            AccountViewModel viewModel = new AccountViewModel() {
                AccountId = account.AccountId,
                Name = account.Name,
                Balence = account.Balence,
                StartBalence = account.StartBalence,
                AccountType = account.AccountTypeId,
                Currency = account.CurrencyId,
                AccountTypes = _context.AccountTypes.ToList(),
                Currencies = _context.Currencies.ToList()
            };

            return View(viewModel);
        }

        // POST: Edit
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AccountViewModel viewModel)
        {
            // If not valid, repopulate the view

            if (!ModelState.IsValid)
            {
                viewModel.AccountTypes = _context.AccountTypes.ToList();
                viewModel.Currencies = _context.Currencies.ToList();
                return View("Edit", viewModel);
            }

            // Update database object

            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            string userId = await _userManager.GetUserIdAsync(user);
            
            Account account = new Account();
            account = _context.Accounts.SingleOrDefault(a => a.AccountId == viewModel.AccountId && a.UserId == userId);

            if (account != null)
            {
                account.UserId = userId;
                account.Name = viewModel.Name;
                account.StartBalence = viewModel.StartBalence;
                account.Balence = viewModel.Balence;
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Accounts");
        }

        // GET: Delete
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            // Load the account that's being deleted
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            string userId = await _userManager.GetUserIdAsync(user);
 
            Account account = _context.Accounts.SingleOrDefault(a => a.AccountId == id && a.UserId == userId);

            AccountViewModel viewModel = new AccountViewModel() {
                AccountId = account.AccountId,
                Name = account.Name
            };

            return View(viewModel);
        }

        // POST: Delete
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(AccountViewModel viewModel)
        {
            // Update database object

            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            string userId = await _userManager.GetUserIdAsync(user);
            
            Account account = new Account();
            account = _context.Accounts.SingleOrDefault(a => a.AccountId == viewModel.AccountId && a.UserId == userId);

            if (account != null)
            {
                _context.Accounts.Remove(account);
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Accounts");
        }
    }
}