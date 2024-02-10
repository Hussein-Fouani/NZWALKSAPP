using Microsoft.EntityFrameworkCore;
using NZWALKS.Models;

namespace NZWALKS.DB
{
    public class NZDBContext:DbContext
    {
        public NZDBContext(DbContextOptions<NZDBContext> option):base(option)
        {

            
        }
        public DbSet<Walks> Walks { get; set; }
        public DbSet<Regions> regions { get; set; }
        public DbSet<Difficulty> difficulties { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //seed data for difficulties
           var diff = new List<Difficulty>(){
                new Difficulty
                {
                    Id= Guid.Parse("a819730b-fe96-49f4-bf91-9d73e560f96d"),
                    Name="Easy"
                },
                 new Difficulty
                 {
                     Id = Guid.Parse("d572fe86-b1b5-42c2-8f27-8f650b44bd4a"),
                     Name = "Easy"
                 },
                  new Difficulty
                  {
                      Id = Guid.Parse("ad5f514d-15a9-42b6-b032-6e000b812ab2"),
                      Name = "Easy"
                  }
            };
            modelBuilder.Entity<Difficulty>().HasData(diff);
            var regions = new List<Regions>()
            {
                new Regions
                {
                    Id = Guid.Parse("57b59cc8-2fe0-4308-8249-c4b18027a080"),
                    Name = "Northland",
                    Code="NL",
                    RegionImageURI="https://www.doc.govt.nz/globalassets/images/places/northland/northland.jpg"

                },
                new Regions
                {
                    Id = Guid.Parse("94c2967b-8dad-46c7-847b-2b233c8d8800"),
                    Name = "Auckland",
                    Code="AK",
                    RegionImageURI="https://www.doc.govt.nz/globalassets/images/places/auckland/auckland.jpg"
                },
                new Regions
                {
                    Id = Guid.Parse("f9331faf-6ac3-4bf8-a0bc-9132bd9f4766"),
                    Name = "Waikato",
                    Code="WK",
                    RegionImageURI="https://www.doc.govt.nz/globalassets/images/places/waikato/waikato.jpg"
                }
            };  
            modelBuilder.Entity<Regions>().HasData(regions);

        }
    }
}
