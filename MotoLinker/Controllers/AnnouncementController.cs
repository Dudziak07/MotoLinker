using Microsoft.AspNetCore.Mvc;
using MotoLinker.Models;

namespace MotoLinker.Controllers;
public class AnnouncementController : Controller
{
    // Statyczna lista og�osze� (na razie bez bazy danych)
    private static List<Announcement> _announcements = new List<Announcement>
    {
        new Announcement
        {
            AnnoucementId = 1,
            Title = "Samoch�d 1",
            Description = "Opis samochodu 1",
            Price = 50000,
            Location = "Warszawa",
            Brand = "BMW",
            Model = "3 Series",
            ProductionYear = 2020,
            UserId = 3 // Przypisz ID u�ytkownika, kt�ry doda� og�oszenie
        },
        new Announcement
        {
            AnnoucementId = 2,
            Title = "Samoch�d 2",
            Description = "Opis samochodu 2",
            Price = 40000,
            Location = "Krak�w",
            Brand = "Audi",
            Model = "A4",
            ProductionYear = 2018,
            UserId = 2 // Przypisz ID u�ytkownika
        },
        new Announcement
        {
            AnnoucementId = 3,
            Title = "Samoch�d 3",
            Description = "Opis samochodu 3",
            Price = 1000,
            Location = "Raciborz",
            Brand = "Z�omek",
            Model = "X1",
            ProductionYear = 2011,
            UserId = 1
        },
        new Announcement
        {
            AnnoucementId = 4,
            Title = "Samoch�d 4",
            Description = "Opis samochodu 4",
            Price = 1000,
            Location = "Raciborz",
            Brand = "Z�omek",
            Model = "X1",
            ProductionYear = 2011,
            UserId = 4
        }
    };

    // Wy�wietlenie listy og�osze�
    public IActionResult List()
    {
        return View(_announcements);
    }

    // Formularz dodawania og�oszenia
    [HttpPost]
    public IActionResult Create(Announcement announcement)
    {
        if (ModelState.IsValid)
        {
            // Pobieranie ID u�ytkownika z sesji
            var userId = HttpContext.Session.GetString("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            // Generowanie nowego ID dla og�oszenia
            announcement.AnnoucementId = _announcements.Count > 0 ? _announcements.Max(a => a.AnnoucementId) + 1 : 1;

            // Przypisanie ID u�ytkownika do pola UserId w og�oszeniu
            announcement.UserId = int.Parse(userId);

            // Dodanie og�oszenia do listy
            _announcements.Add(announcement);

            TempData["Message"] = "Og�oszenie zosta�o dodane.";
            return RedirectToAction("List");
        }

        return View(announcement);
    }

    public IActionResult Details(int id)
    {
        // Znajd� og�oszenie na podstawie ID
        var announcement = _announcements.FirstOrDefault(a => a.AnnoucementId == id);

        if (announcement == null)
        {
            // Je�li og�oszenia nie ma, zwr�� stron� 404
            return NotFound();
        }

        // Przeka� og�oszenie do widoku
        return View(announcement);
    }

    // GET: Announcement/Edit/5
    public IActionResult Edit(int id)
    {
        var userId = HttpContext.Session.GetString("UserId");
        if (userId == null) return RedirectToAction("Login", "Auth");

        var isAdmin = HttpContext.Session.GetString("IsAdmin") == "True";
        var announcement = _announcements.FirstOrDefault(a => a.AnnoucementId == id);

        if (announcement == null || (announcement.UserId != int.Parse(userId) && !isAdmin))
        {
            return Forbid(); // Zablokuj dost�p, je�li u�ytkownik nie ma uprawnie�
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
        var announcement = _announcements.FirstOrDefault(a => a.AnnoucementId == id);

        if (announcement == null || (announcement.UserId != int.Parse(userId) && !isAdmin))
        {
            return Forbid(); // Zablokuj dost�p, je�li u�ytkownik nie ma uprawnie�
        }

        if (id != updatedAnnouncement.AnnoucementId)
        {
            return BadRequest();
        }

        if (ModelState.IsValid)
        {
            // Aktualizacja w�a�ciwo�ci og�oszenia
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