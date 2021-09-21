using System;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using SportStore.Infrastructure;

namespace SportStore.Models
{
    public class SessionCart : CartBase
    {
        [JsonIgnore]
        public ISession Session { get; set; }

        public static CartBase GetCart(IServiceProvider service)
        {
            var session = service.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            var cart = session?.GetJson<SessionCart>("cart") ?? new SessionCart();
            cart.Session = session;

            return cart;
        }

        public override void AddItem(Product product, int quantity)
        {
            base.AddItem(product, quantity);
            Session.SetJson("cart", this);
        }

        public override void RemoveLine(Product product)
        {
            base.RemoveLine(product);
            Session.SetJson("cart", this);
        }

        public override void Clear()
        {
            base.Clear();
            Session.SetJson("cart", this);
        }
    }
}
