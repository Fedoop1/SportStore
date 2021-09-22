using System.Linq;

namespace SportStore.Models.Repo
{
    public interface IStoreRepository
    {
        public IQueryable<Product> Products { get; }
        public void SaveProduct(Product product);
        void CreateProduct(Product product);
        void DeleteProduct(Product product);
    }
}
