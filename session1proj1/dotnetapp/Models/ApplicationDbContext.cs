using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace dotnetapp.Models
{
    public partial class BookDbContext : DbContext
    {
        public BookDbContext()
        {
        }

        public BookDbContext(DbContextOptions<BookDbContext> options)
            : base(options)
        {
        }

        public DbSet<Comment> Comments { get; set; }
    }
}
