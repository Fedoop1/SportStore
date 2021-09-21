using Microsoft.AspNetCore.Mvc;
using SportStore.Models;

namespace SportStore.Components
{
    public class CartSummaryViewComponent : ViewComponent
    {
        private readonly CartBase cart;

        public CartSummaryViewComponent(CartBase cart) => this.cart = cart;

        public IViewComponentResult Invoke() => View(cart);
    }
}
