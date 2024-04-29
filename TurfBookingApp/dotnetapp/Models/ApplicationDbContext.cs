using Microsoft.EntityFrameworkCore;

namespace dotnetapp.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        
        // Update DbSet properties to represent turfs instead of dining tables
        public DbSet<Turf> Turfs { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define the relationship between the Turf and Booking classes - one-to-many
            modelBuilder.Entity<Turf>()
                .HasMany(t => t.Bookings)
                .WithOne(b => b.Turf)
                .HasForeignKey(b => b.TurfID);

            base.OnModelCreating(modelBuilder);

            // Seed data for Turfs
            modelBuilder.Entity<Turf>().HasData(
                new Turf { TurfID = 1, Name = "Turf A", Capacity = 4, Availability = true },
                new Turf { TurfID = 2, Name = "Turf B", Capacity = 6, Availability = true },
                new Turf { TurfID = 3, Name = "Turf C", Capacity = 2, Availability = true },
                new Turf { TurfID = 4, Name = "Turf D", Capacity = 10, Availability = false },
                new Turf { TurfID = 5, Name = "Turf E", Capacity = 2, Availability = true },
                new Turf { TurfID = 6, Name = "Turf F", Capacity = 2, Availability = false }
            );
        }
    }
}
