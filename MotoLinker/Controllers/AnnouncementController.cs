using Microsoft.AspNetCore.Mvc;
using MotoLinker.Models;

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
            ProductionYear = 2020
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
            ProductionYear = 2018
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
            ProductionYear = 2011
        }
    };

    // Wyœwietlenie listy og³oszeñ
    public IActionResult List()
    {
        return View(_announcements);
    }

    // Formularz dodawania og³oszenia
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(Announcement announcement)
    {
        if (ModelState.IsValid)
        {
            // Dodanie og³oszenia do listy (na razie bez bazy danych)
            announcement.Id = _announcements.Count + 1;
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
}
