using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MotoLinker.Models;

namespace MotoLinker.Models
{
    public class AttributeValue
    {
        public int Id { get; set; } // ID wartoœci atrybutu
        public string Key { get; set; } // Klucz atrybutu (np. "Mileage", "FuelType")
        public string Value { get; set; } // Wartoœæ atrybutu w formie tekstowej
        public AttributeType Type { get; set; } // Typ atrybutu
    }

    public enum AttributeType
    {
        ShortText,
        LongText,
        Integer,
        Real,
        Boolean
    }

    public class Announcement : IValidatableObject
    {
        public List<AttributeValue> Attributes { get; set; } = new List<AttributeValue>();
        public int AnnouncementId { get; set; } // ID og³oszenia

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public int Price { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        public string Brand { get; set; }

        [Required]
        public string Model { get; set; }

        [Required]
        [Range(1886, int.MaxValue, ErrorMessage = "Rok produkcji nie mo¿e byæ mniejszy ni¿ 1886.")]
        public int ProductionYear { get; set; }

        public DateTime DateAdded { get; set; } = DateTime.Now; // Data dodania og³oszenia

        public int UserId { get; set; } // Powi¹zanie og³oszenia z u¿ytkownikiem

        // Walidacja dynamiczna
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (ProductionYear > DateTime.Now.Year)
            {
                yield return new ValidationResult(
                    $"Rok produkcji nie mo¿e byæ wiêkszy ni¿ {DateTime.Now.Year}.",
                    new[] { nameof(ProductionYear) });
            }
        }

        public List<Category> Categories { get; set; } = new List<Category>();

        public List<int> SelectedCategoryIds { get; set; } = new List<int>(); // ID wybranych kategorii
    }
}

public class Category
{
    public int CategoryId { get; set; }
    public string Name { get; set; }

    // Relacja wiele-do-wielu
    public List<Announcement> Announcements { get; set; } = new List<Announcement>();
}