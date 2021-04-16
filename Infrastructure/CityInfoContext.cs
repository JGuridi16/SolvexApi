using Microsoft.EntityFrameworkCore;
using DataAccess.Entities;

namespace DataAccess
{
    public class CityInfoContext : DbContext
    {
        public DbSet<City> Cities { get; set; }
        public DbSet<PointOfInterest> PointsOfInterest { get; set; }

        public CityInfoContext(DbContextOptions<CityInfoContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>()
                .HasData(
                    new City()
                    {
                        Id = 1,
                        Name = "New York City",
                        Description = "Best city in the world"
                    },
                    new City()
                    {
                        Id = 2,
                        Name = "Santo Domingo",
                        Description = "Hottest city in the world"
                    },
                    new City()
                    {
                        Id = 3,
                        Name = "Paris",
                        Description = "World’s most important and attractive city"
                    }
                );

            modelBuilder.Entity<PointOfInterest>()
                .HasData(
                    new PointOfInterest()
                    {
                        Id = 1,
                        Name = "Central Park",
                        Description = "Best park in New York",
                        CityId = 1
                    },
                    new PointOfInterest()
                    {
                        Id = 2,
                        Name = "Jardin Botanico",
                        Description = "Good place to relax",
                        CityId = 2
                    },
                    new PointOfInterest()
                    {
                        Id = 3,
                        Name = "Empire State Building",
                        Description = "One of the most important and relevant building in New York",
                        CityId = 1
                    }
                );

            base.OnModelCreating(modelBuilder);
        }
    }
}
