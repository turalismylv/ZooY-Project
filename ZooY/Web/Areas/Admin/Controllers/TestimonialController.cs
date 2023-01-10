using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.Testimonial;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class TestimonialController : Controller
    {
        private readonly ITestimonialService _testimonialService;

        public TestimonialController(ITestimonialService testimonialService)
        {
            _testimonialService = testimonialService;
        }

        #region Testimonial


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = await _testimonialService.GetAllAsync();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Create(TestimonialCreateVM model)
        {
            var isSucceeded = await _testimonialService.CreateAsync(model);
            if (isSucceeded) return RedirectToAction(nameof(Index));
            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var model = await _testimonialService.GetUpdateModelAsync(id);
            if (model == null) return NotFound();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, TestimonialUpdateVM model)
        {
            if (id != model.Id) return NotFound();

            var isSucceeded = await _testimonialService.UpdateAsync(model);
            if (isSucceeded) return RedirectToAction(nameof(Index));

            model = await _testimonialService.GetUpdateModelAsync(id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var isSucceded = await _testimonialService.DeleteAsync(id);
            if (isSucceded)
            {
                return RedirectToAction(nameof(Index));
            }
            return NotFound();


        }

        #endregion
    }
}
