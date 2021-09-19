using Microsoft.EntityFrameworkCore;

namespace SportStore.Models.Repo
{
    public class StoreRepository : IStoreRepository
    {
        private readonly StoreDbContext context;
        public StoreRepository(StoreDbContext context) => this.context = context;

        public DbSet<Product> Products => this.context.Products;
    }
}
