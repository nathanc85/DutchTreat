using System;
using DutchTreat.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace DutchTreat.Data
{
    public class DutchContext : DbContext
    {
        public DutchContext(DbContextOptions<DutchContext> options): base(options)
        {

        }

        public DbSet<Product> Products
        {
            get;
            set;
        }
        public DbSet<Order> Orders
        {
            get;
            set;
        }

        public DutchContext()
        {
        }

        // How the mapping happens between our entities and database.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Order>()
                        .HasData(new Order()
            {
                Id = 1,
                OrderDate = DateTime.UtcNow,
                OrderNumber = "12345"
            });
            //modelBuilder.Entity<Product>()
                        //.Property(p => p.Title)
                        //.HasMaxLength(50);
        }
    }
}
