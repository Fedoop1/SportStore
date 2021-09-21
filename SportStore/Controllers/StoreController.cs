using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SportStore.Models;
using SportStore.Models.Repo;
using SportStore.Models.ViewModels;

namespace SportStore.Controllers
{
    public class StoreController : Controller
    {
        private readonly IProductRepository repository;

        public StoreController(IProductRepository repository) => this.repository = repository ??
            throw new ArgumentNullException(nameof(repository), "Store repository can't be null");

        public IActionResult Index(string category, int pageIndex = 1)
        {
            const int pageSize = 4;

            Func<Product, bool> predicate = category is null
                ? x => true
                : x => x.Category == category;

            return View(new ProductsListViewModel()
            {
                PagingInfo = new PagingInfo()
                {
                    CurrentPage = pageSize, ItemsPerPage = pageSize,
                    TotalItems = string.IsNullOrEmpty(category)
                        ? repository.Products.Count()
                        : repository.Products.Count(x => x.Category == category)
                },

                Products = repository.Products.Where(predicate)
                    .OrderBy(x => x.ProductId).Skip((pageIndex - 1) * pageSize).Take(pageSize),
                CurrentCategory = category
            });
        }

        public IActionResult Privacy() => View();
    }
}
