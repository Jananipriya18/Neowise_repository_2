// using Microsoft.EntityFrameworkCore;
// using dotnetapp.Models;

// namespace dotnetapp.Data
// {
//     public class ApplicationDbContext : DbContext
//     {
//         public DbSet<User> Users { get; set; }
//         public DbSet<Customer> Customers { get; set; }
//         public DbSet<Product> Products { get; set; }
//         public DbSet<Orders> Orders { get; set; }
//         public DbSet<Cart> Carts { get; set; }

//         public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
//         {
//         }

//         protected override void OnModelCreating(ModelBuilder modelBuilder)
//         {
//             modelBuilder.Entity<Customer>()
//                 .HasOne(c => c.User)
//                 .WithOne()
//                 .HasForeignKey<Customer>(c => c.UserId);

//             modelBuilder.Entity<Product>()
//                 .HasOne(g => g.Cart)
//                 .WithMany(c => c.Products)
//                 .HasForeignKey(g => g.CartId)
//                 .OnDelete(DeleteBehavior.Restrict);

//             modelBuilder.Entity<Product>()
//                 .HasOne(g => g.Orders)
//                 .WithMany(o => o.Products)
//                 .HasForeignKey(g => g.OrdersId)
//                 .OnDelete(DeleteBehavior.Restrict);

//             modelBuilder.Entity<Orders>()
//                 .HasOne(o => o.Customer)
//                 .WithMany(c => c.Orders)
//                 .HasForeignKey(o => o.CustomerId)
//                 .OnDelete(DeleteBehavior.Restrict);

//             modelBuilder.Entity<Cart>()
//                 .HasOne(c => c.Customer)
//                 .WithOne()
//                 .HasForeignKey<Cart>(c => c.CustomerId);
//         }
//     }
// }
using Microsoft.AspNetCore.Identity.EntityFrameworkCore; // Add this using directive
using Microsoft.EntityFrameworkCore;
using dotnetapp.Models;
using Microsoft.AspNetCore.Identity;
namespace dotnetapp.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser> // Inherit from IdentityDbContext<IdentityUser>
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Order> Orders { get; set; }
 
 
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
                base.OnModelCreating(modelBuilder);
 
            modelBuilder.Entity<Customer>()
                .HasOne(c => c.User)
                .WithOne()
                .HasForeignKey<Customer>(c => c.UserId);
 
            modelBuilder.Entity<Cart>()
                .HasOne(c => c.Customer)
                .WithOne()
                .HasForeignKey<Cart>(c => c.CustomerId);
 
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Customer)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.CustomerId);
 
            modelBuilder.Entity<Order>()
                .HasMany(o => o.Products)
                .WithOne(g => g.Order)
                .HasForeignKey(g => g.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}