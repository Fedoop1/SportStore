using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SportStore.Models.Repo
{
    public class StoreRepository : IStoreRepository
    {
        private readonly StoreDbContext context;
        public StoreRepository(StoreDbContext context) => this.context = context;

        public IQueryable<Product> Products => this.context.Products;

        public async void SaveProduct(Product product)
        {
            this.context.Entry(product).State = EntityState.Modified;
            await this.context.SaveChangesAsync();
        }

        public void CreateProduct(Product product)
        {
            this.context.Products.Add(product);
            this.context.SaveChanges();
        }

        public void DeleteProduct(Product product)
        {
            var productToDelete = this.context.Products.Find(product.ProductId);
            this.context.Products.Remove(productToDelete);
        }
    }
}
