//ApplicationDbContext.cs
using Microsoft.EntityFrameworkCore;
using dotnetapp.Models;

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

            // Seed data
            modelBuilder.Entity<Train>().HasData(
                new Train { TrainID = 1, DepartureLocation = "Delhi", Destination = "Mumbai", DepartureTime = DateTime.Now.AddHours(2), MaximumCapacity = 4 },
                new Train { TrainID = 2, DepartureLocation = "Chennai", Destination = "Kolkata", DepartureTime = DateTime.Now.AddHours(3), MaximumCapacity = 3 },
                new Train { TrainID = 3, DepartureLocation = "Bangalore", Destination = "Hyderabad", DepartureTime = DateTime.Now.AddHours(9), MaximumCapacity = 2 },
                new Train { TrainID = 4, DepartureLocation = "Jaipur", Destination = "Ahmedabad", DepartureTime = DateTime.Now.AddHours(5), MaximumCapacity = 4 },
                new Train { TrainID = 5, DepartureLocation = "Lucknow", Destination = "Bhopal", DepartureTime = DateTime.Now.AddHours(7), MaximumCapacity = 3 }
            );
        }
    }
}
