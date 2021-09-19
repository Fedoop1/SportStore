using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SportStore.Models.Repo;
using SportStore.Models.ViewModels;

namespace SportStore.Controllers
{
    public class StoreController : Controller
    {
        private readonly IStoreRepository repository;

        public StoreController(IStoreRepository repository) => this.repository = repository ??
            throw new ArgumentNullException(nameof(repository), "Store repository can't be null");

        public IActionResult Index(int pageIndex = 1)
        {
            const int pageSize = 4;
            return View(new ProductsListViewModel()
            {
                PagingInfo = new PagingInfo()
                    {CurrentPage = pageSize, ItemsPerPage = pageSize, TotalItems = repository.Products.Count()},
                Products = repository.Products.OrderBy(x => x.ProductId).Skip((pageIndex - 1) * pageSize).Take(pageSize)
            });
        }

        public IActionResult Privacy() => View();
    }
}
