using Microsoft.EntityFrameworkCore;

namespace SportStore.Models.Repo
{
    public interface IStoreRepository
    {
        public DbSet<Product> Products { get; }
    }
}
