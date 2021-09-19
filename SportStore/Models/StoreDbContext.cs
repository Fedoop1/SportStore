using Microsoft.EntityFrameworkCore;

namespace SportStore.Models
{
    public class StoreDbContext : DbContext
    {
        public StoreDbContext(DbContextOptions<StoreDbContext> options) 
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .Property(x => x.Price)
                .HasColumnType("decimal(8,2)");
        }

        public DbSet<Product> Products { get; set; }
    }
}
