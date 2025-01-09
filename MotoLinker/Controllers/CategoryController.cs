using Microsoft.AspNetCore.Mvc;
using MotoLinker.Models;
using System.Collections.Generic;
using System.Linq;

namespace MotoLinker.Controllers
{
    public class CategoryController : Controller
    {
        // Statyczna lista kategorii (zastępuje bazę danych)
        private static List<Category> _categories = new List<Category>
        {
            new Category { CategoryId = 1, Name = "Samochody osobowe" },
            new Category { CategoryId = 2, Name = "Samochody dostawcze" },
            new Category { CategoryId = 3, Name = "Motocykle" }
        };

        // Wyświetlenie listy kategorii
        public IActionResult Index()
        {
            return View(_categories);
        }

        // Formularz tworzenia nowej kategorii (GET)
        [HttpGet]
        public IActionResult Create()
        {
            // Przekazanie istniejących kategorii do widoku (np. dla wyboru nadrzędnej kategorii)
            ViewBag.Categories = _categories;
            return View();
        }

        // Obsługa przesłania formularza tworzenia nowej kategorii (POST)
        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                // Generowanie nowego ID dla kategorii
                category.CategoryId = _categories.Count > 0 ? _categories.Max(c => c.CategoryId) + 1 : 1;

                // Dodanie kategorii do listy
                _categories.Add(category);

                return RedirectToAction(nameof(Index));
            }

            // Jeśli model jest nieprawidłowy, zwróć formularz z błędami
            return View(category);
        }
    }
}