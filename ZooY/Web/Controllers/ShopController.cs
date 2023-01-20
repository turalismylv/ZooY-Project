using Microsoft.AspNetCore.Mvc;
using Web.Services.Abstract;
using Web.ViewModels.Shop;

namespace Web.Controllers
{
    public class ShopController : Controller
    {
        private readonly IShopService _shopService;

        public ShopController(IShopService shopService)
        {

            _shopService = shopService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(ShopIndexVM model)
        {
            model = await _shopService.GetAllAsync(model);
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var model = await _shopService.GetAsync(id);
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> CategoryProduct(int id)
        {
            var model = await _shopService.CategoryProductAsync(id);

            return PartialView("_CategoryProductPartial", model);

        }

        [HttpGet]
        public async Task<IActionResult> BrandProduct(int id)
        {
            var model = await _shopService.BrandProductAsync(id);

            return PartialView("_BrandProductPartial", model);

        }
        [HttpGet]
        public async Task<IActionResult> TagProduct(int id)
        {
            var model = await _shopService.TagProductAsync(id);

            return PartialView("_TagProductPartial", model);

        }
    }
}

