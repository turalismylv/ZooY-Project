using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.Welcome;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class WelcomeController : Controller
    {

        private readonly IWelcomeService _welcomeService;

        public WelcomeController(IWelcomeService welcomeService)
        {


            _welcomeService = welcomeService;
        }
        [HttpGet]

        public async Task<IActionResult> Index()
        {
            var model = await _welcomeService.GetAsync();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var welcome = await _welcomeService.GetAsync();

            if (welcome.Welcome != null)
            {
                return NotFound();
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(WelcomeCreateVM model)
        {


            var isSucceeded = await _welcomeService.CreateAsync(model);
            if (isSucceeded) return RedirectToAction(nameof(Index));
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var model = await _welcomeService.GetUpdateModelAsync(id);
            if (model == null) return NotFound();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, WelcomeUpdateVM model)
        {
            if (id != model.Id) return NotFound();

            var isSucceeded = await _welcomeService.UpdateAsync(model);
            if (isSucceeded) return RedirectToAction(nameof(Index));

            model = await _welcomeService.GetUpdateModelAsync(id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var isSucceded = await _welcomeService.DeleteAsync(id);
            if (isSucceded)
            {
                return RedirectToAction(nameof(Index));
            }
            return NotFound();

        }

    }
}
