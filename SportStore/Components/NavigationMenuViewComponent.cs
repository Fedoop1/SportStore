using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SportStore.Models.Repo;

namespace SportStore.Components
{
    public class NavigationMenuViewComponent : ViewComponent
    {
        private readonly IProductRepository repository;

        public NavigationMenuViewComponent(IProductRepository repository) => this.repository = repository;

        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedCategory = RouteData?.Values["category"];
            return View(repository.Products.Select(x => x.Category).Distinct().OrderBy(x => x));
        }
    }
}
