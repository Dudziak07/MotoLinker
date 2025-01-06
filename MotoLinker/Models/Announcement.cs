using System.ComponentModel.DataAnnotations;

namespace MotoLinker.Models
{
    public class Announcement
    {
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
        [Range(1886, int.MaxValue, ErrorMessage = "Rok produkcji musi byæ pomiêdzy 1886 a bie¿¹cym rokiem.")]
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
        public DateTime DateAdded { get; set; } = DateTime.Now; // Data dodania og³oszenia
        public int UserId { get; set; } // Powi¹zanie og³oszenia z u¿ytkownikiem
    }
}