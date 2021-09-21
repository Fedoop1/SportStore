using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportStore.Models;
using SportStore.Models.Repo;

namespace SportStore.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderRepository repository;
        private readonly CartBase cartService;

        public OrderController(IOrderRepository repository, CartBase cartService) => (this.repository, this.cartService) = (repository, cartService);

        public IActionResult Index() => RedirectToAction("Index", controllerName: "Cart", new {returnUrl = "/"});

        [HttpGet]
        public IActionResult Checkout() => View(new Order());

        [HttpPost]
        [ActionName("Checkout")]
        public async Task<IActionResult> CheckoutSubmitted(Order order)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (!this.cartService.CartLines.Any())
            {
                ModelState.AddModelError("", "Sorry, your cart is empty!");
                return View(order);
            }

            try
            {
                await this.repository.SaveOrder(order);
            }
            catch (DbUpdateConcurrencyException)
            {
                ModelState.AddModelError("", "An exception occurred during processing your order. Try again or complete order later.");
                return View();
            }
            
            this.cartService.Clear();
            return View("Completed", order.OrderId);
        }
    }
}
