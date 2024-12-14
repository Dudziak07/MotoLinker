using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MotoLinker.Models
{
    public class MyDBContext : DbContext
    {
        public MyDBContext() : base("MyCS") { }

        public DbSet<User> Users { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Bid> Bids { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Transaction>()
                .HasRequired(t => t.Vehicle)
                .WithMany()
                .HasForeignKey(t => t.VehicleID);

            modelBuilder.Entity<Transaction>()
                .HasRequired(t => t.Seller)
                .WithMany()
                .HasForeignKey(t => t.SellerID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Transaction>()
                .HasRequired(t => t.Buyer)
                .WithMany()
                .HasForeignKey(t => t.BuyerID)
                .WillCascadeOnDelete(false);
        }
    }
}