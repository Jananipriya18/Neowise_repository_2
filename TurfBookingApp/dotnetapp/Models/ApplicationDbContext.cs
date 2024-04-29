using Microsoft.EntityFrameworkCore;

namespace dotnetapp.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<DiningTable> DiningTables { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        // Define the DbSet properties for the DiningTable and Booking classes - DiningTables and Bookings

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define the relationship between the DiningTable and Booking classes - one-to-many
            modelBuilder.Entity<DiningTable>()
                .HasMany(t => t.Bookings)
                .WithOne(b => b.DiningTable)
                .HasForeignKey(b => b.DiningTableID);
                
            base.OnModelCreating(modelBuilder);

            // Seed data for DiningTables
            modelBuilder.Entity<DiningTable>().HasData(
                new DiningTable { DiningTableID = 1, SeatingCapacity = 4, Availability = true },
                new DiningTable { DiningTableID = 2, SeatingCapacity = 6, Availability = true },
                new DiningTable { DiningTableID = 3, SeatingCapacity = 2, Availability = true },
                new DiningTable { DiningTableID = 4, SeatingCapacity = 10, Availability = false },
                new DiningTable { DiningTableID = 5, SeatingCapacity = 2, Availability = true },
                new DiningTable { DiningTableID = 6, SeatingCapacity = 2, Availability = false }
            );
        }
    }
}
