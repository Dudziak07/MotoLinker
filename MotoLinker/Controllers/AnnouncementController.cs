using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MotoLinker.Models;

namespace MotoLinker.Controllers;
public class AnnouncementController : Controller
{
    private static List<MotoLinker.Models.Category> _categories = new List<MotoLinker.Models.Category>
    {
        new MotoLinker.Models.Category { CategoryId = 1, Name = "Samochody osobowe" },
        new MotoLinker.Models.Category { CategoryId = 2, Name = "Samochody dostawcze" },
        new MotoLinker.Models.Category { CategoryId = 3, Name = "Motocykle" }
    };


    // Statyczna lista og³oszeñ (na razie bez bazy danych)
    private static List<Announcement> _announcements = new List<Announcement>
    {
        new Announcement
        {
            AnnouncementId = 1,
            Title = "BMW 3 Series super nowoczesny!",
            Description = "Opis samochodu 1",
            Price = 50000,
            Location = "Warszawa",
            Brand = "BMW",
            Model = "3 Series",
            ProductionYear = 2020,
            UserId = 3, // Przypisz ID u¿ytkownika, który doda³ og³oszenie
            Categories = new List<MotoLinker.Models.Category> { _categories[0] } // Powi¹zanie z kategori¹ "Samochody osobowe"
        },
        new Announcement
        {
            AnnouncementId = 2,
            Title = "Audi A4 dla ka¿dego szpanera!!!",
            Description = "Opis samochodu 2",
            Price = 40000,
            Location = "Kraków",
            Brand = "Audi",
            Model = "A4",
            ProductionYear = 2018,
            UserId = 2,
            Categories = new List<MotoLinker.Models.Category> { _categories[0] }
        },
        new Announcement
        {
            AnnouncementId = 3,
            Title = "Z³omek dla ubo¿szych",
            Description = "Opis samochodu 3",
            Price = 1000,
            Location = "Raciborz",
            Brand = "Z³omek",
            Model = "X1",
            ProductionYear = 2011,
            UserId = 1,
            Categories = new List<MotoLinker.Models.Category> { _categories[0], _categories[1] }
        },
        new Announcement
        {
            AnnouncementId = 4,
            Title = "Samochód marzeñ wprost z AUT (Z³omek)",
            Description = "Opis samochodu 4",
            Price = 100000,
            Location = "W³oc³awek",
            Brand = "Z³omek",
            Model = "Car2",
            ProductionYear = 1890,
            UserId = 4,
            Categories = new List<MotoLinker.Models.Category> { _categories[0], _categories[1], _categories[2] }
        }
    };
    public static List<Announcement> GetAnnouncements()
    {
        return _announcements;
    }

    // Wyœwietlenie listy og³oszeñ
    public IActionResult List()
    {
        ViewBag.Categories = _categories; // Przypisanie listy kategorii
        return View(_announcements);
    }

    // GET: Announcement/Create
    public IActionResult Create()
    {
        ViewBag.Categories = _categories; // Przekazywanie kategorii do widoku
        ViewBag.AttributeTypes = Enum.GetValues(typeof(AttributeType)).Cast<AttributeType>();
        return View();
    }

    // POST: Announcement/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Announcement announcement)
    {
        if (ModelState.IsValid)
        {
            // Przypisanie wybranych kategorii
            announcement.Categories = _categories
                .Where(c => announcement.SelectedCategoryIds.Contains(c.CategoryId))
                .ToList();

            // Jeœli lista atrybutów jest pusta, ustaw pust¹ listê
            if (announcement.Attributes == null)
            {
                announcement.Attributes = new List<AttributeValue>();
            }

            announcement.AnnouncementId = _announcements.Count > 0 ? _announcements.Max(a => a.AnnouncementId) + 1 : 1;
            var userId = HttpContext.Session.GetString("UserId");
            if (userId != null) announcement.UserId = int.Parse(userId);

            _announcements.Add(announcement);

            TempData["Message"] = "Og³oszenie zosta³o dodane.";
            return RedirectToAction("List");
        }

        ViewBag.Categories = _categories;
        ViewBag.AttributeTypes = Enum.GetValues(typeof(AttributeType)).Cast<AttributeType>();
        return View(announcement);
    }

    public IActionResult Details(int id)
    {
        // ZnajdŸ og³oszenie na podstawie ID
        var announcement = _announcements.FirstOrDefault(a => a.AnnouncementId == id);

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
        var announcement = _announcements.FirstOrDefault(a => a.AnnouncementId == id);

        if (announcement == null || (announcement.UserId != int.Parse(userId) && !isAdmin))
        {
            return Forbid();
        }

        announcement.SelectedCategoryIds = announcement.Categories.Select(c => c.CategoryId).ToList();

        ViewBag.Categories = _categories;

        // Przekazanie atrybutów do widoku
        ViewBag.AttributeTypes = Enum.GetValues(typeof(AttributeType)).Cast<AttributeType>();
        return View(announcement);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, Announcement updatedAnnouncement)
    {
        var userId = HttpContext.Session.GetString("UserId");
        if (userId == null) return RedirectToAction("Login", "Auth");

        var isAdmin = HttpContext.Session.GetString("IsAdmin") == "True";
        var announcement = _announcements.FirstOrDefault(a => a.AnnouncementId == id);

        if (announcement == null || (announcement.UserId != int.Parse(userId) && !isAdmin))
        {
            return Forbid();
        }

        if (id != updatedAnnouncement.AnnouncementId)
        {
            return BadRequest();
        }

        if (ModelState.IsValid)
        {
            // Aktualizacja kategorii
            announcement.Categories = _categories
                .Where(c => updatedAnnouncement.SelectedCategoryIds.Contains(c.CategoryId))
                .ToList();

            // Aktualizacja w³aœciwoœci og³oszenia
            announcement.Title = updatedAnnouncement.Title;
            announcement.Description = updatedAnnouncement.Description;
            announcement.Price = updatedAnnouncement.Price;
            announcement.Location = updatedAnnouncement.Location;
            announcement.Brand = updatedAnnouncement.Brand;
            announcement.Model = updatedAnnouncement.Model;
            announcement.ProductionYear = updatedAnnouncement.ProductionYear;

            // Obs³uga atrybutów
            if (updatedAnnouncement.Attributes == null)
            {
                updatedAnnouncement.Attributes = new List<AttributeValue>();
            }

            // Aktualizacja atrybutów
            if (updatedAnnouncement.Attributes != null)
            {
                announcement.Attributes = updatedAnnouncement.Attributes;
            }

            TempData["Message"] = "Og³oszenie zosta³o zaktualizowane.";
            return RedirectToAction(nameof(List));
        }

        ViewBag.Categories = _categories;
        ViewBag.AttributeTypes = Enum.GetValues(typeof(AttributeType)).Cast<AttributeType>();
        return View(updatedAnnouncement);
    }

    // POST: Announcement/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        var userId = HttpContext.Session.GetString("UserId");
        if (userId == null) return RedirectToAction("Login", "Auth");

        var isAdmin = HttpContext.Session.GetString("IsAdmin") == "True";
        var announcement = _announcements.FirstOrDefault(a => a.AnnouncementId == id);

        if (announcement == null || (announcement.UserId != int.Parse(userId) && !isAdmin))
        {
            return Forbid(); // Zablokuj dostêp, jeœli u¿ytkownik nie ma uprawnieñ
        }

        // Usuñ og³oszenie z listy
        _announcements.Remove(announcement);

        TempData["Message"] = "Og³oszenie zosta³o usuniête.";
        return RedirectToAction("List");
    }

    public IActionResult ByCategory(int id)
    {
        // Filtrowanie og³oszeñ wed³ug kategorii
        var announcements = _announcements
            .Where(a => a.Categories.Any(c => c.CategoryId == id))
            .ToList();

        ViewBag.Categories = _categories; // Przekazanie kategorii do widoku
        ViewBag.CurrentCategoryId = id; // Przekazanie aktualnej kategorii

        return View("List", announcements);
    }


    public IActionResult ListAll()
    {
        // Wyœwietlenie wszystkich og³oszeñ
        ViewBag.Categories = _categories; // Przekazanie kategorii do widoku

        return View("List", _announcements);
    }
}