using Microsoft.AspNetCore.Mvc;
using MotoLinker.Models;

namespace MotoLinker.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            // Przykładowe dane ogłoszeń
            var latestAnnouncements = new List<Announcement>
            {
                new Announcement { AnnouncementId = 1, Title = "Samochód 1", Description = "Opis 1"},
                new Announcement { AnnouncementId = 2, Title = "Samochód 2", Description = "Opis 2"},
                new Announcement { AnnouncementId = 3, Title = "Samochód 3", Description = "Opis 3"}
            };

            return View(new HomeIndexViewModel
            {
                LatestAnnouncements = latestAnnouncements
            });
        }
    }
}