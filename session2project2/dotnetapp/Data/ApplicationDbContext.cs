using dotnetapp.Models;
using System.Collections.Generic;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;

namespace dotnetapp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<GroceryItem> GroceryItems { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GroceryItem>()
                .HasKey(g => g.ItemId); //ItemId is the primary key

            modelBuilder.Entity<GroceryItem>().Property(g => g.ItemName).IsRequired();
            modelBuilder.Entity<GroceryItem>().Property(g => g.Price).HasColumnType("decimal(18,2)");
                
        }

    }
}

