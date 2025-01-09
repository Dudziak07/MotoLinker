using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using MotoLinker.Models;
using System.Linq;

namespace MotoLinker.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            // Pobranie danych z AnnouncementController
            var latestAnnouncements = AnnouncementController.GetAnnouncements()
                .OrderByDescending(a => a.DateAdded)
                .Take(3)
                .ToList();

            return View(new HomeIndexViewModel
            {
                LatestAnnouncements = latestAnnouncements
            });
        }
        public IActionResult ChangeLanguage(string culture, string returnUrl)
        {
            if (!string.IsNullOrEmpty(culture))
            {
                HttpContext.Session.SetString("CurrentCulture", culture);
                Response.Cookies.Append(
                    CookieRequestCultureProvider.DefaultCookieName,
                    CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                    new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
                );
            }

            return LocalRedirect(returnUrl);
        }
    }
}