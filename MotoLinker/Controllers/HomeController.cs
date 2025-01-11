using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MotoLinker.Data;
using MotoLinker.Models;
using System.Linq;

namespace MotoLinker.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private List <Announcement> _announcements;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
            _announcements = _context.Announcements.Include(a => a.Categories).ToList();
           
        }
        public IActionResult Index()
        {
            // Pobranie danych z AnnouncementController
            var latestAnnouncements = _announcements
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