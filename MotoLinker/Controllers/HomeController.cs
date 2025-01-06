using Microsoft.AspNetCore.Mvc;
using MotoLinker.Models;
using System.Linq;

namespace MotoLinker.Controllers
{
    public class HomeController : Controller
    {
        private static List<Announcement> _announcements = new List<Announcement>
        {
            new Announcement { AnnouncementId = 1, Title = "Samochód 1", Description = "Opis samochodu 1", DateAdded = DateTime.Now.AddDays(-1) },
            new Announcement { AnnouncementId = 2, Title = "Samochód 2", Description = "Opis samochodu 2", DateAdded = DateTime.Now.AddDays(-2) },
            new Announcement { AnnouncementId = 3, Title = "Samochód 3", Description = "Opis samochodu 3", DateAdded = DateTime.Now.AddDays(-3) },
            new Announcement { AnnouncementId = 4, Title = "Samochód 4", Description = "Opis samochodu 4", DateAdded = DateTime.Now.AddDays(-4) }
        };

        public IActionResult Index()
        {
            // Pobierz 3 najnowsze ogłoszenia
            var latestAnnouncements = _announcements
                .OrderByDescending(a => a.DateAdded)
                .Take(3)
                .ToList();

            return View(new HomeIndexViewModel
            {
                LatestAnnouncements = latestAnnouncements
            });
        }
    }
}