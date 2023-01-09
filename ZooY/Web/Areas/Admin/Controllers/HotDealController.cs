using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.HotDeal;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class HotDealController : Controller
    {

        private readonly IHotDealService _hotDealService;

        public HotDealController(IHotDealService hotDealService)
        {


            _hotDealService = hotDealService;
        }
        [HttpGet]

        public async Task<IActionResult> Index()
        {
            var model = await _hotDealService.GetAsync();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var hotDeal = await _hotDealService.GetAsync();

            if (hotDeal.HotDeal != null)
            {
                return NotFound();
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(HotDealCreateVM model)
        {
            var isSucceeded = await _hotDealService.CreateAsync(model);
            if (isSucceeded) return RedirectToAction(nameof(Index));
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var model = await _hotDealService.GetUpdateModelAsync(id);
            if (model == null) return NotFound();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, HotDealUpdateVM model)
        {
            if (id != model.Id) return NotFound();

            var isSucceeded = await _hotDealService.UpdateAsync(model);
            if (isSucceeded) return RedirectToAction(nameof(Index));

            model = await _hotDealService.GetUpdateModelAsync(id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var isSucceded = await _hotDealService.DeleteAsync(id);
            if (isSucceded)
            {
                return RedirectToAction(nameof(Index));
            }
            return NotFound();

        }

    }
}
