using System.ComponentModel.DataAnnotations;

namespace MotoLinker.Models // Upewnij siê, ¿e przestrzeñ nazw jest poprawna
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
        public int ProductionYear { get; set; }

        public Guid UserID { get; set; }

        // Formatowanie ID jako 000-000-001
        public string FormattedId => $"{Id:D9}".Insert(3, "-").Insert(7, "-");
    }
}