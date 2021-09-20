using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SportStore.Infrastructure;
using SportStore.Models;
using SportStore.Models.Repo;
using SportStore.Models.ViewModels;

namespace SportStore.Controllers
{
    public class CartController : Controller
    {
        private readonly IStoreRepository repository;
        public CartController(IStoreRepository repository) => this.repository = repository;

        [HttpGet]
        public IActionResult Index(string returnUrl) => View(new CartViewModel()
            {Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart(), ReturnUrl = returnUrl ?? "/"});

        [HttpPost]
        public async Task<IActionResult> Index(int productId, string returnUrl)
        {
            var product = await this.repository.Products.FindAsync(productId);
            var cart = this.HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
            cart.AddItem(product, 1);
            HttpContext.Session.SetJson("cart", cart);

            return View(new CartViewModel() {Cart = cart, ReturnUrl = returnUrl});
        }
    }
}
