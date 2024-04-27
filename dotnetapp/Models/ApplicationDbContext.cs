// ApplicationDbContext.cs
using Microsoft.EntityFrameworkCore;
using dotnetapp.Models;
using System;

namespace dotnetapp.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Train> Trains { get; set; }
        public DbSet<Passenger> Passengers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Train>()
                .HasMany(t => t.Passengers)
                .WithOne(p => p.Train)
                .HasForeignKey(p => p.TrainID);

            // Custom departure dates
            var departure_date1 = new DateTime(2024, 1, 15);
            var departure_date2 = new DateTime(2023, 2, 20);
            var departure_date3 = new DateTime(2024, 2, 20);
            var departure_date4 = new DateTime(2024, 5, 20);

            // Seed data
            modelBuilder.Entity<Train>().HasData(
                new Train { TrainID = 1, DepartureLocation = "Delhi", Destination = "Mumbai", DepartureTime = departure_date1, MaximumCapacity = 4 },
                new Train { TrainID = 2, DepartureLocation = "Chennai", Destination = "Kolkata", DepartureTime = departure_date2, MaximumCapacity = 3 },
                new Train { TrainID = 3, DepartureLocation = "Bangalore", Destination = "Hyderabad", DepartureTime = departure_date3, MaximumCapacity = 2 },
                new Train { TrainID = 4, DepartureLocation = "Jaipur", Destination = "Ahmedabad", DepartureTime = departure_date4, MaximumCapacity = 4 }
            );
        }
    }
}
