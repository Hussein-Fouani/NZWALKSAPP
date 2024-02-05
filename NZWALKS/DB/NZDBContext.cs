using Microsoft.EntityFrameworkCore;
using NZWALKS.Models;

namespace NZWALKS.DB
{
    public class NZDBContext:DbContext
    {
        public NZDBContext(DbContextOptions option):base(option)
        {

            
        }
        public DbSet<Walks> Walks { get; set; }
        public DbSet<Regions> regions { get; set; }
        public DbSet<Difficulty> difficulties { get; set; }

    }
}
