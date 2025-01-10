using Microsoft.AspNetCore.Mvc;
using MotoLinker.Models;

namespace MotoLinker.Controllers
{
    public class AdminController : Controller
    {
        private static List<User> _users = new List<User>
        {
            new User { UserId = 1, Username = "admin", Email = "admin@example.com", Password = "admin", IsAdmin = true },
            new User { UserId = 2, Username = "user1", Email = "user1@example.com", Password = "password1", IsAdmin = false },
            new User { UserId = 3, Username = "user2", Email = "user2@example.com", Password = "password2", IsAdmin = false }
        };

        public IActionResult ManageUsers()
        {
            if (HttpContext.Session.GetString("IsAdmin") != "True")
            {
                return Forbid();
            }

            return View("ManageUsers", _users);
        }

        public IActionResult EditUser(int id)
        {
            var user = _users.FirstOrDefault(u => u.UserId == id);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditUser(int id, User updatedUser)
        {
            var user = _users.FirstOrDefault(u => u.UserId == id);

            if (user == null)
            {
                return NotFound();
            }

            if (!string.IsNullOrWhiteSpace(updatedUser.Username))
            {
                user.Username = updatedUser.Username;
            }

            if (!string.IsNullOrWhiteSpace(updatedUser.Email))
            {
                user.Email = updatedUser.Email;
            }

            // Jeśli hasło zostanie podane, aktualizuj je
            if (!string.IsNullOrWhiteSpace(updatedUser.Password))
            {
                user.Password = updatedUser.Password;
            }

            TempData["Message"] = "Dane użytkownika zostały zaktualizowane.";
            return RedirectToAction("Index");
        }

        public IActionResult DeleteUser(int id)
        {
            var user = _users.FirstOrDefault(u => u.UserId == id);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [HttpPost, ActionName("DeleteUser")]
        public IActionResult DeleteUserConfirmed(int id)
        {
            var user = _users.FirstOrDefault(u => u.UserId == id);

            if (user == null)
            {
                return NotFound();
            }

            _users.Remove(user);
            TempData["Message"] = "Użytkownik został usunięty.";
            return RedirectToAction("Index");
        }

        public IActionResult MakeAdmin(int id)
        {
            var user = _users.FirstOrDefault(u => u.UserId == id);

            if (user == null)
            {
                return NotFound();
            }

            user.IsAdmin = true;
            TempData["Message"] = $"Użytkownik {user.Username} został administratorem.";
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ToggleAdmin(int id)
        {
            var user = _users.FirstOrDefault(u => u.UserId == id);

            if (user == null || user.UserId == 1) // Zablokowanie zmiany dla konta admina
            {
                return Forbid();
            }

            user.IsAdmin = !user.IsAdmin;

            TempData["Message"] = user.IsAdmin
                ? "Użytkownik został administratorem."
                : "Uprawnienia administratora zostały odebrane.";

            return RedirectToAction("Index");
        }

        public IActionResult ManageCategories()
        {
            if (HttpContext.Session.GetString("IsAdmin") != "True")
            {
                return Forbid();
            }
            // Dodaj logikę do wyświetlania kategorii
            return View("ManageCategories");
        }
    }
}
