using System.Linq;
using System.Threading.Tasks;

namespace SportStore.Models.Repo
{
    public interface IOrderRepository
    {
        public IQueryable<Order> Orders { get; }
        public Task SaveOrder(Order order);
    }
}
