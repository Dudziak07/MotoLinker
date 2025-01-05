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
        public int Price { get; set; }

        [Required]
        public string Location { get; set; }

        public DateTime DateAdded { get; set; } = DateTime.Now;

        [Required]
        public string Brand { get; set; }

        [Required]
        public string Model { get; set; }

        [Required]
        [Range(1886, int.MaxValue, ErrorMessage = "Rok produkcji musi byæ pomiêdzy 1886 a {1}.")]
        public int ProductionYear
        {
            get => _productionYear;
            set
            {
                if (value > DateTime.Now.Year)
                {
                    throw new ValidationException($"Rok produkcji nie mo¿e byæ wiêkszy ni¿ {DateTime.Now.Year}.");
                }
                _productionYear = value;
            }
        }
        private int _productionYear;
    }
}