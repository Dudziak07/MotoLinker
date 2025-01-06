using Microsoft.AspNetCore.Mvc;
using MotoLinker.Models;

namespace MotoLinker.Controllers;
public class AnnouncementController : Controller
{
    // Statyczna lista og³oszeñ (na razie bez bazy danych)
    private static List<Announcement> _announcements = new List<Announcement>
    {
        new Announcement
        {
            Id = 1,
            Title = "Samochód 1",
            Description = "Opis samochodu 1",
            Price = 50000,
            Location = "Warszawa",
            Brand = "BMW",
            Model = "3 Series",
            ProductionYear = 2020,
            UserId = 1 // Przypisz ID u¿ytkownika, który doda³ og³oszenie
        },
        new Announcement
        {
            Id = 2,
            Title = "Samochód 2",
            Description = "Opis samochodu 2",
            Price = 40000,
            Location = "Kraków",
            Brand = "Audi",
            Model = "A4",
            ProductionYear = 2018,
            UserId = 2 // Przypisz ID u¿ytkownika
        },
        new Announcement
        {
            Id = 3,
            Title = "Samochód 3",
            Description = "Opis samochodu 3",
            Price = 1000,
            Location = "Raciborz",
            Brand = "Z³omek",
            Model = "X1",
            ProductionYear = 2011,
            UserId = 1
        },
        new Announcement
        {
            Id = 4,
            Title = "Samochód 3",
            Description = "Opis samochodu 3",
            Price = 1000,
            Location = "Raciborz",
            Brand = "Z³omek",
            Model = "X1",
            ProductionYear = 2011,
            UserId = 3
        }
    };

    // Wyœwietlenie listy og³oszeñ
    public IActionResult List()
    {
        return View(_announcements);
    }

    // Formularz dodawania og³oszenia
    [HttpPost]
    public IActionResult Create(Announcement announcement)
    {
        if (ModelState.IsValid)
        {
            var userId = HttpContext.Session.GetString("UserId");
            if (userId == null) return RedirectToAction("Login", "Auth");

            // Generowanie nowego ID dla og³oszenia
            announcement.Id = _announcements.Count + 1;

            // Przypisz ID u¿ytkownika z sesji do og³oszenia
            announcement.UserId = int.Parse(userId);
            _announcements.Add(announcement);

            TempData["Message"] = "Og³oszenie zosta³o dodane.";
            return RedirectToAction("List");
        }

        return View(announcement);
    }

    public IActionResult Details(int id)
    {
        // ZnajdŸ og³oszenie na podstawie ID
        var announcement = _announcements.FirstOrDefault(a => a.Id == id);

        if (announcement == null)
        {
            // Jeœli og³oszenia nie ma, zwróæ stronê 404
            return NotFound();
        }

        // Przeka¿ og³oszenie do widoku
        return View(announcement);
    }

    // GET: Announcement/Edit/5
    public IActionResult Edit(int id)
    {
        var userId = HttpContext.Session.GetString("UserId");
        if (userId == null) return RedirectToAction("Login", "Auth");

        var isAdmin = HttpContext.Session.GetString("IsAdmin") == "True";
        var announcement = _announcements.FirstOrDefault(a => a.Id == id);

        if (announcement == null || (announcement.UserId != int.Parse(userId) && !isAdmin))
        {
            return Forbid(); // Zablokuj dostêp, jeœli u¿ytkownik nie ma uprawnieñ
        }

        return View(announcement);
    }

    // POST: Announcement/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, Announcement updatedAnnouncement)
    {
        var userId = HttpContext.Session.GetString("UserId");
        if (userId == null) return RedirectToAction("Login", "Auth");

        var isAdmin = HttpContext.Session.GetString("IsAdmin") == "True";
        var announcement = _announcements.FirstOrDefault(a => a.Id == id);

        if (announcement == null || (announcement.UserId != int.Parse(userId) && !isAdmin))
        {
            return Forbid(); // Zablokuj dostêp, jeœli u¿ytkownik nie ma uprawnieñ
        }

        if (id != updatedAnnouncement.Id)
        {
            return BadRequest();
        }

        if (ModelState.IsValid)
        {
            // Aktualizacja w³aœciwoœci og³oszenia
            announcement.Title = updatedAnnouncement.Title;
            announcement.Description = updatedAnnouncement.Description;
            announcement.Price = updatedAnnouncement.Price;
            announcement.Location = updatedAnnouncement.Location;
            announcement.Brand = updatedAnnouncement.Brand;
            announcement.Model = updatedAnnouncement.Model;
            announcement.ProductionYear = updatedAnnouncement.ProductionYear;

            return RedirectToAction(nameof(List));
        }

        return View(updatedAnnouncement);
    }
}