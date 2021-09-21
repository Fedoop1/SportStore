using System.Linq;

namespace SportStore.Models.Repo
{
    public interface IProductRepository
    {
        public IQueryable<Product> Products { get; }
        public void SaveProduct(Product product);
    }
}
