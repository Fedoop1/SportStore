using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SportStore.Models.Repo
{
    public class OrderRepository : IOrderRepository
    {
        private readonly StoreDbContext context;

        public OrderRepository(StoreDbContext context) => this.context = context;

        public IQueryable<Order> Orders => this.context.Orders
            .Include(x => x.Lines)
            .ThenInclude(x => x.Product);

        public IQueryable<CartLine> CartLines { get; }

        public async Task SaveOrder(Order order)
        {
            this.context.Orders.Attach(order);
            await context.SaveChangesAsync();
        }
    }
}
