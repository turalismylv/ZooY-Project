using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.HomeMainSlider;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class HomeMainSliderController : Controller
    {

        private readonly IHomeMainSliderService _homeMainSliderService;

        public HomeMainSliderController(IHomeMainSliderService homeMainSliderService)
        {
            _homeMainSliderService = homeMainSliderService;
        }

        #region HomeMainSlider


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = await _homeMainSliderService.GetAllAsync();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Create(HomeMainSliderCreateVM model)
        {
            var isSucceeded = await _homeMainSliderService.CreateAsync(model);
            if (isSucceeded) return RedirectToAction(nameof(Index));
            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var model = await _homeMainSliderService.GetUpdateModelAsync(id);
            if (model == null) return NotFound();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, HomeMainSliderUpdateVM model)
        {
            if (id != model.Id) return NotFound();

            var isSucceeded = await _homeMainSliderService.UpdateAsync(model);
            if (isSucceeded) return RedirectToAction(nameof(Index));

            model = await _homeMainSliderService.GetUpdateModelAsync(id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var isSucceded = await _homeMainSliderService.DeleteAsync(id);
            if (isSucceded)
            {
                return RedirectToAction(nameof(Index));
            }
            return NotFound();


        }

        #endregion
    }
}
