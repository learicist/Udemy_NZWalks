using Microsoft.EntityFrameworkCore;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Data
{
    public class NZWalksDbContext: DbContext
    {
        public NZWalksDbContext(DbContextOptions<NZWalksDbContext> dbContextOptions): base(dbContextOptions) 
        {
            
        }

        public DbSet<Difficulty> Difficulties { get; set; }

        public DbSet<Region> Regions { get; set; }

        public DbSet<Walk> Walks { get; set; }

        public DbSet<Image> Images { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Seed data for Difficulties - Easy, Med, Hard
            var difficulties = new List<Difficulty>() 
            {
                new Difficulty()
                {
                    Id = Guid.Parse("1d3c98ae-6c68-44b3-b1af-f0384e899f18"),
                    Name = "Easy",
                },
                new Difficulty()
                {
                    Id = Guid.Parse("4a659fbe-43d0-4edd-b88a-6c74bd3ae55d"),
                    Name = "Medium",
                },
                new Difficulty()
                {
                    Id = Guid.Parse("ee171b45-96a1-4d96-babb-d14c273c6d04"),
                    Name = "Hard",
                }
            };

            //Seed the list to the DB
            modelBuilder.Entity<Difficulty>().HasData(difficulties);


            var regions = new List<Region>()
            {
                new Region()
                {
                    Id = Guid.Parse("523ee4a1-2547-4fba-8de8-7e96585223d9"),
                    Name = "Auckland",
                    Code = "AKL",
                    RegionImageUrl = "image.jpg"
                },
                new Region()
                {
                    Id = Guid.Parse("53e7a6d5-2065-4977-8784-e383cffdce36"),
                    Name = "Northland",
                    Code = "NTL",
                    RegionImageUrl = "image2.jpg"
                },
                new Region()
                {
                    Id = Guid.Parse("2be60b15-a0ad-48d9-96b1-67482b429070"),
                    Name = "Bay Of Plenty",
                    Code = "BOP",
                    RegionImageUrl = "image3.jpg"
                },
                new Region()
                {
                    Id = Guid.Parse("e56c6ae9-fe62-4b6b-b96d-aed3b428ce62"),
                    Name = "Wellington",
                    Code = "WGN",
                    RegionImageUrl = "image4.jpg"
                },
                new Region()
                {
                    Id = Guid.Parse("bab3cb4c-677e-4ca5-a1b8-a7aa359c1a0a"),
                    Name = "Nelson",
                    Code = "WGN",
                    RegionImageUrl = "image5.jpg"
                },
                new Region()
                {
                    Id = Guid.Parse("63012100-48c1-4cbb-9e5b-b0fdeffca151"),
                    Name = "Southland",
                    Code = "STL",
                    RegionImageUrl = null
                },
            };

            modelBuilder.Entity<Region>().HasData(regions);
        }

    }
}
