using Microsoft.EntityFrameworkCore;
using MotoLinker.Models;

namespace MotoLinker.Data
{
    public class MyDBContext : DbContext
    {
        public MyDBContext(DbContextOptions<MyDBContext> options)
            : base(options)
        {
        }

        public DbSet<Announcement> Announcements { get; set; }
        public DbSet<Bid> Bids { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }

        // Opcjonalne: konfiguracja modelu za pomocą Fluent API
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Przykład konfiguracji modelu:
            modelBuilder.Entity<Announcement>()
                .Property(a => a.Title)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
