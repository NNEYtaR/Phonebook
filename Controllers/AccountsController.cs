using Microsoft.AspNetCore.Mvc;
using Phonebook.Data;
using Phonebook.Models;

namespace Phonebook.Controllers
{
    public class AccountsController : Controller
    {
        private ApplicationDbContext _context;
    
        public AccountsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index() 
        {
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View("Register", new Account());
        }

        [HttpPost]
        public IActionResult Register(Account account)
        {
            account.Password = BCrypt.Net.BCrypt.HashPassword(account.Password);
            _context.Accounts.Add(account);
            _context.SaveChanges();
            return RedirectToAction("Login");
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var account = checkAccount(username, password);
            if (account == null)
            {
                ViewBag.error = "Invalid";
                return View("Login");
            }
            else
            {
                HttpContext.Session.SetString("username", username);
                return RedirectToAction("Index", "Home", new { area = "" });
            }
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("username");
            return RedirectToAction("Login");
        }

        private Account? checkAccount(string username, string password)
        {
            var account = _context.Accounts.SingleOrDefault(x => x.Username.Equals(username));
            if (account != null)
            {
                if (BCrypt.Net.BCrypt.Verify(password, account.Password))
                {
                    return account;
                }
            }
            return null;
        }
    }
}