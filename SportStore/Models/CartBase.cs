using System;
using System.Collections.Generic;
using System.Linq;

namespace SportStore.Models
{
    public class CartBase
    {
        public List<CartLine> CartLines { get; set; } = new();

        public virtual void AddItem(Product product, int quantity)
        {
            var cartLine = CartLines.FirstOrDefault(p => p.ProductId == product.ProductId);
            if (cartLine is not null)
            {
                cartLine.Quantity += quantity;
                return;
            }

            CartLines.Add(new CartLine() { ProductId = product.ProductId, Product = product, Quantity = quantity});
        }

        public virtual void RemoveLine(Product product)
        {
            CartLines.RemoveAll(cartLine => cartLine.Product.ProductId == product.ProductId);
        }
        public virtual void Clear() => this.CartLines.Clear();
        public decimal ComputeTotalValue() => CartLines.Sum(x => x.Product.Price * x.Quantity);
        
    }
}
