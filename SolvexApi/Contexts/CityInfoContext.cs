using Microsoft.EntityFrameworkCore;
using SolvexApi.Entities;

namespace SolvexApi.Contexts
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
                    }
                );

            modelBuilder.Entity<PointOfInterest>()
                .HasData(
                    new PointOfInterest()
                    {
                        Id = 1,
                        CityId = 1,
                        Name = "Central Park",
                        Description = "Best park in New York"
                    },
                    new PointOfInterest()
                    {
                        Id = 2,
                        CityId = 2,
                        Name = "Jardin Botanico",
                        Description = "Good place to relax"
                    }
                );

            base.OnModelCreating(modelBuilder);
        }
    }
}