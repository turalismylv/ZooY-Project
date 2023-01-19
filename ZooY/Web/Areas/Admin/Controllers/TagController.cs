using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.Tag;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class TagController : Controller
    {
        private readonly ITagService _tagService;

        public TagController(ITagService tagService)
        {
            _tagService = tagService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = await _tagService.GetAllAsync();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Create(TagCreateVM model)
        {
            var isSucceeded = await _tagService.CreateAsync(model);
            if (isSucceeded) return RedirectToAction(nameof(Index));
            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var model = await _tagService.GetUpdateModelAsync(id);
            if (model == null) return NotFound();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, TagUpdateVM model)
        {
            if (id != model.Id) return NotFound();

            var isSucceeded = await _tagService.UpdateAsync(model);
            if (isSucceeded) return RedirectToAction(nameof(Index));

            model = await _tagService.GetUpdateModelAsync(id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var isSucceded = await _tagService.DeleteAsync(id);
            if (isSucceded)
            {
                return RedirectToAction(nameof(Index));
            }
            return NotFound();

        }



        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var model = await _tagService.GetDetailsModelAsync(id);
            if (model == null) return NotFound();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> AddProducts(int id)
        {
            var model = await _tagService.GetTagAddProductsModel(id);
            if (model == null) return NotFound();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddProducts(int id, TagAddProductsVM model)
        {
            if (id != model.TagId) return BadRequest();

            var isSucceeded = await _tagService.AddProductsAsync(model);
            if (isSucceeded) return RedirectToAction(nameof(Index));

            model = await _tagService.GetTagAddProductsModel(model.TagId);
            return View(model);
        }
    }
}
