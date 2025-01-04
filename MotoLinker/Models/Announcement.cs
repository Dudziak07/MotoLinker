using System;
using System.ComponentModel.DataAnnotations;

namespace MotoLinker.Models
{
    public class Announcement
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Required]
        public string Location { get; set; }

        public DateTime DateAdded { get; set; } = DateTime.Now;

        public string Brand { get; set; }

        public string Model { get; set; }

        [Required]
        [CustomProductionYearValidation] /// Niestandardowy atrybut
        public int ProductionYear { get; set; }
    }

    /// Niestandardowy atrybut walidacji dla roku produkcji.
    public class CustomProductionYearValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is int year)
            {
                int currentYear = DateTime.Now.Year;

                if (year < 1886 || year > currentYear)
                {
                    return new ValidationResult($"Rok produkcji musi byæ pomiêdzy 1886 a {currentYear}.");
                }
            }

            return ValidationResult.Success;
        }
    }
}