using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace SportStore.Models.Repo
{
    public class OrderRepository : IOrderRepository
    {
        private readonly StoreDbContext context;
        private readonly ILogger logger;

        public OrderRepository(StoreDbContext context, ILogger<OrderRepository> logger) => (this.context, this.logger) = (context, logger);

        public IQueryable<Order> Orders => this.context.Orders
            .Include(x => x.Lines)
            .ThenInclude(x => x.Product);

        public async Task SaveOrder(Order order)
        {
            this.context.Orders.Add(order);
            var result = await context.SaveChangesAsync();
            logger.LogInformation("Order successfully add!");
        }
    }
}
