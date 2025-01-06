using System.ComponentModel.DataAnnotations;

namespace MotoLinker.Models
{
    public class Announcement
    {
        public int AnnouncementId { get; set; } // ID og�oszenia
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
        [Range(1886, int.MaxValue, ErrorMessage = "Rok produkcji musi by� pomi�dzy 1886 a bie��cym rokiem.")]
        public int ProductionYear
        {
            get => _productionYear;
            set
            {
                if (value > DateTime.Now.Year)
                {
                    throw new ValidationException($"Rok produkcji nie mo�e by� wi�kszy ni� {DateTime.Now.Year}.");
                }
                _productionYear = value;
            }
        }
        private int _productionYear;
        public DateTime DateAdded { get; set; } = DateTime.Now; // Data dodania og�oszenia
        public int UserId { get; set; } // Powi�zanie og�oszenia z u�ytkownikiem
    }
}