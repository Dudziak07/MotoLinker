using System.Collections.Generic;

namespace MotoLinker.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }

        // Relacja nadrzędna (dla drzewa)
        public int? ParentCategoryId { get; set; }
        public Category ParentCategory { get; set; }

        // Relacja podrzędna (dla drzewa)
        public ICollection<Category> ChildCategories { get; set; }

        // Relacja wiele do wielu z ogłoszeniami
        public ICollection<Announcement> Announcements { get; set; }
    }
}