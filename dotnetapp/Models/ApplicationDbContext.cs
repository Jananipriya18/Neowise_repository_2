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
                new Train { TrainID = 1, DepartureLocation = "Location1", Destination = "Destination1", DepartureTime = DateTime.Now, MaximumCapacity = 4 },
                new Train { TrainID = 2, DepartureLocation = "Location2", Destination = "Destination2", DepartureTime = DateTime.Now, MaximumCapacity = 3 },
                new Train { TrainID = 3, DepartureLocation = "Location3", Destination = "Destination3", DepartureTime = DateTime.Now, MaximumCapacity = 2 },
                new Train { TrainID = 4, DepartureLocation = "Location4", Destination = "Destination4", DepartureTime = DateTime.Now, MaximumCapacity = 4 },
                new Train { TrainID = 5, DepartureLocation = "Location5", Destination = "Destination5", DepartureTime = DateTime.Now, MaximumCapacity = 3 }
            );
        }
    }
}
