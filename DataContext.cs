using LocationFilter.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocationFilter
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Location> Locations { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Location>()
                .HasData(new Location
                {
                    Id = 1,
                    Address = "Old Delhi Market",
                    City = "New Delhi",
                    State = "Delhi",
                    Zip = "123456"
                },
                new Location
                {
                    Id = 2,
                    Address = "South Beach",
                    City = "Miami",
                    State = "Florida",
                    Zip = "123457"
                },
                new Location
                {
                    Id = 3,
                    Address = "Fort Lauderdale",
                    City = "Miami",
                    State = "Florida",
                    Zip = "123458"
                },
                new Location
                {
                    Id = 4,
                    Address = "Fort Worth",
                    City = "Dallas",
                    State = "Texas",
                    Zip = "123459"
                },
                new Location
                {
                    Id = 5,
                    Address = "North Fort",
                    City = "Miami Beach",
                    State = "Florida",
                    Zip = "123489"
                });

            builder.Entity<Location>()
            .HasIndex(b => new { b.Address, b.City, b.State, b.Zip })
            .IsUnique();
        }

    }
}
