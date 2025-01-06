using Microsoft.AspNetCore.Mvc;
using MotoLinker.Models;
using System.Linq;

namespace MotoLinker.Controllers
{
    public class AuthController : Controller
    {
        private static List<User> _users = new List<User>
        {
            new User { Username = "admin", Password = "admin", IsAdmin = true },
            new User { Username = "user1", Password = "password1", IsAdmin = false }
        };

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var user = _users.FirstOrDefault(u => u.Username == username && u.Password == password);
            if (user != null)
            {
                HttpContext.Session.SetString("UserId", user.Id.ToString());
                HttpContext.Session.SetString("IsAdmin", user.IsAdmin.ToString());
                HttpContext.Session.SetString("Username", user.Username);
                return RedirectToAction("List", "Announcement");
            }

            ViewBag.Error = "Nieprawidłowy login lub hasło.";
            return View();
        }

        public IActionResult Logout(string returnUrl = "/")
        {
            HttpContext.Session.Clear();
            return Redirect(returnUrl ?? "/");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                _users.Add(user); // Automatyczne przypisanie ID przez konstruktor
                TempData["Message"] = "Rejestracja zakończona sukcesem. Możesz się zalogować.";
                return RedirectToAction("Login");
            }

            return View(user);
        }
    }
}