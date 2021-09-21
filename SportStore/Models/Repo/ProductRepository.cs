using System.Linq;

namespace SportStore.Models.Repo
{
    public class ProductRepository : IProductRepository
    {
        private readonly StoreDbContext context;
        public ProductRepository(StoreDbContext context) => this.context = context;

        public IQueryable<Product> Products => this.context.Products;

        public async void SaveProduct(Product product)
        {
            this.context.Products.Add(product);
            await this.context.SaveChangesAsync();
        }
    }
}
