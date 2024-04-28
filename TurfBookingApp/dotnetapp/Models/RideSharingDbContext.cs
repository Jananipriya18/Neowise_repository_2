using Microsoft.EntityFrameworkCore;


namespace RideShare.Models
{

public class RideSharingDbContext : DbContext
{
    public RideSharingDbContext(DbContextOptions<RideSharingDbContext> options) : base(options)
    {
    }

    public DbSet<Ride> Rides { get; set; }
    public DbSet<Commuter> Commuters { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Ride>()
            .HasMany(r => r.Commuters)
            .WithOne(c => c.Ride)
            .HasForeignKey(c => c.RideID);

        // Seed data
        modelBuilder.Entity<Ride>().HasData(
            new Ride { RideID = 1, DepartureLocation = "Location1", Destination = "Destination1", DateOfDeparture = DateTime.Now, MaximumCapacity = 4 },
            new Ride { RideID = 2, DepartureLocation = "Location2", Destination = "Destination2", DateOfDeparture = DateTime.Now, MaximumCapacity = 3 },
            new Ride { RideID = 3, DepartureLocation = "Location3", Destination = "Destination3", DateOfDeparture = DateTime.Now, MaximumCapacity = 2 },
            new Ride { RideID = 4, DepartureLocation = "Location4", Destination = "Destination4", DateOfDeparture = DateTime.Now, MaximumCapacity = 4 },
            new Ride { RideID = 5, DepartureLocation = "Location5", Destination = "Destination5", DateOfDeparture = DateTime.Now, MaximumCapacity = 3 }
        );
    }
}
}
