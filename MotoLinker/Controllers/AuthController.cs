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
            new User { Username = "user1", Password = "user1", IsAdmin = false },
            new User { Username = "user2", Password = "user2", IsAdmin = false }
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
                HttpContext.Session.SetString("UserId", user.UserId.ToString());
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
                // Sprawdź unikalność nazwy użytkownika
                if (_users.Any(u => u.Username == user.Username))
                {
                    ModelState.AddModelError("Username", "Ten login jest już zajęty.");
                    return View(user);
                }

                // Sprawdź unikalność emaila
                if (_users.Any(u => u.Email == user.Email))
                {
                    ModelState.AddModelError("Email", "Ten email jest już zajęty.");
                    return View(user);
                }

                // Dodaj użytkownika do listy
                _users.Add(user);
                TempData["Message"] = "Rejestracja zakończona sukcesem. Możesz się zalogować.";
                return RedirectToAction("Login");
            }

            return View(user);
        }
    }
}