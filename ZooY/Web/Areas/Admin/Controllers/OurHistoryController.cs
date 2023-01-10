using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.OurHistory;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class OurHistoryController : Controller
    {

        private readonly IOurHistoryService _ourHistoryService;

        public OurHistoryController(IOurHistoryService ourHistoryService)
        {


            _ourHistoryService = ourHistoryService;
        }
        [HttpGet]

        public async Task<IActionResult> Index()
        {
            var model = await _ourHistoryService.GetAsync();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var ourHistory = await _ourHistoryService.GetAsync();

            if (ourHistory.OurHistory != null)
            {
                return NotFound();
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(OurHistoryCreateVM model)
        {


            var isSucceeded = await _ourHistoryService.CreateAsync(model);
            if (isSucceeded) return RedirectToAction(nameof(Index));
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var model = await _ourHistoryService.GetUpdateModelAsync(id);
            if (model == null) return NotFound();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, OurHistoryUpdateVM model)
        {
            if (id != model.Id) return NotFound();

            var isSucceeded = await _ourHistoryService.UpdateAsync(model);
            if (isSucceeded) return RedirectToAction(nameof(Index));

            model = await _ourHistoryService.GetUpdateModelAsync(id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var isSucceded = await _ourHistoryService.DeleteAsync(id);
            if (isSucceded)
            {
                return RedirectToAction(nameof(Index));
            }
            return NotFound();

        }

    }
}
