using dotnetapp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace dotnetapp.Data
{
    public class ApplicationDbContext:IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users {  get; set; }
        public DbSet<AgroChemicals> AgroChemicals { get; set; }
        public DbSet<Crop> Crops { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }

        public DbSet<Request> Requests { get; set; }
    }
}
