using Microsoft.AspNetCore.Mvc;
using MotoLinker.Models;

public class AnnouncementController : Controller
{
    // Statyczna lista og�osze� (na razie bez bazy danych)
    private static List<Announcement> _announcements = new List<Announcement>
    {
        new Announcement
        {
            Id = 1,
            Title = "Samoch�d 1",
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
            Title = "Samoch�d 2",
            Description = "Opis samochodu 2",
            Price = 40000,
            Location = "Krak�w",
            Brand = "Audi",
            Model = "A4",
            ProductionYear = 2018
        }
    };

    // Wy�wietlenie listy og�osze�
    public IActionResult List()
    {
        return View(_announcements);
    }

    // Formularz dodawania og�oszenia
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(Announcement announcement)
    {
        if (ModelState.IsValid)
        {
            // Dodanie og�oszenia do listy (na razie bez bazy danych)
            announcement.Id = _announcements.Count + 1;
            _announcements.Add(announcement);

            TempData["Message"] = "Og�oszenie zosta�o dodane.";
            return RedirectToAction("List");
        }

        return View(announcement);
    }
}
