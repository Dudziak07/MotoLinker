using Microsoft.AspNetCore.Mvc;
using MotoLinker.Models;
using System.Linq;

namespace MotoLinker.Controllers
{
    public class AuthController : Controller
    {
        private static List<User> _users = new List<User>
        {
             new User { Id = Guid.NewGuid(), Username = "admin", Password = "admin", IsAdmin = true },
             new User { Id = Guid.NewGuid(), Username = "user1", Password = "password1", IsAdmin = false }
        };


        // GET: Logowanie
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
                return RedirectToAction("List", "Announcement");
            }

            ViewBag.Error = "Nieprawidłowy login lub hasło.";
            return View();
        }

        // Wylogowanie
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        // Rejestracja
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                user.Id = Guid.NewGuid(); // Ustaw unikalne ID
                _users.Add(user); // Dodaj użytkownika do listy (lub bazy danych)
                TempData["Message"] = "Rejestracja zakończona sukcesem. Możesz się zalogować.";
                return RedirectToAction("Login");
            }

            return View(user);
        }
    }
}