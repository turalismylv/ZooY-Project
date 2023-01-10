using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.Brand;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class BrandController : Controller
    {

        private readonly IBrandService _brandService;

        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        #region HomeMainSlider


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = await _brandService.GetAllAsync();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Create(BrandCreateVM model)
        {
            var isSucceeded = await _brandService.CreateAsync(model);
            if (isSucceeded) return RedirectToAction(nameof(Index));
            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var model = await _brandService.GetUpdateModelAsync(id);
            if (model == null) return NotFound();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, BrandUpdateVM model)
        {
            if (id != model.Id) return NotFound();

            var isSucceeded = await _brandService.UpdateAsync(model);
            if (isSucceeded) return RedirectToAction(nameof(Index));

            model = await _brandService.GetUpdateModelAsync(id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var isSucceded = await _brandService.DeleteAsync(id);
            if (isSucceded)
            {
                return RedirectToAction(nameof(Index));
            }
            return NotFound();


        }

        #endregion
    }
}

