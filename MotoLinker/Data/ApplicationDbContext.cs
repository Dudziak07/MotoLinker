using Microsoft.EntityFrameworkCore;
using MotoLinker.Models;

namespace MotoLinker.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Announcement> Announcements { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }


        // Opcjonalne: konfiguracja modelu za pomocą Fluent API
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Przykład konfiguracji modelu:
            modelBuilder.Entity<Announcement>()
            .HasMany(a => a.Categories)
            .WithMany(c => c.Announcements)
            .UsingEntity(j => j.ToTable("AnnouncementCategories"));
        }
    }
}
