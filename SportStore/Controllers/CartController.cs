using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SportStore.Models;
using SportStore.Models.Repo;
using SportStore.Models.ViewModels;

namespace SportStore.Controllers
{
    public class CartController : Controller
    {
        private readonly IStoreRepository repository;
        private readonly CartBase cartService;
        public CartController(IStoreRepository repository, CartBase cartService) => (this.repository, this.cartService) = (repository, cartService);

        [HttpGet]
        public IActionResult Index(string returnUrl) => View(new CartViewModel() {Cart = cartService, ReturnUrl = returnUrl ?? "/"});

        [HttpGet]
        public PartialViewResult AddToCart([FromQuery]int productId)
        {
            var product = this.repository.Products.FirstOrDefault(x => x.ProductId == productId);
            this.cartService.AddItem(product, 1);
            return PartialView("Components/CartSummary/Default", cartService);
        }

        [HttpPost]
        public IActionResult Remove(int productId, string returnUrl)
        {
            var product = this.repository.Products.FirstOrDefault(x => x.ProductId == productId);

            if (product is null)
            {
                return BadRequest();
            }

            cartService.RemoveLine(product);
            return RedirectToAction("Index", new {returnUrl = returnUrl ?? "/"});
        }
    }
}
