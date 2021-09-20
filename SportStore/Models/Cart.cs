using System;
using System.Collections.Generic;
using System.Linq;

namespace SportStore.Models
{
    public class Cart
    {
        public List<CartLine> CartLines { get; set; } = new();

        public void AddItem(Product product, int quantity)
        {
            var cartLine = CartLines.FirstOrDefault(p => p.Product.ProductId == product.ProductId);
            if (cartLine is not null)
            {
                cartLine.Quantity += quantity;
                return;
            }

            CartLines.Add(new CartLine() { Product = product, Quantity = quantity });
        }

        public void RemoveLine(Product product)
        {
            if (product is null)
            {
                throw new ArgumentNullException(nameof(product), "Product can't be null");
            }

            CartLines.RemoveAll(cartLine => cartLine.Product.ProductId == product.ProductId);
        }
        public decimal ComputeTotalValue() => CartLines.Sum(x => x.Product.Price * x.Quantity);
        public void Clear() => this.CartLines.Clear();
    }
}
