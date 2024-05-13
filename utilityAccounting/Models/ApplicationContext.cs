using Microsoft.EntityFrameworkCore;

namespace utilityAccounting.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Building> Buildings { get; set; }
        public DbSet<Stage> Stages { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
        }
    }
}
