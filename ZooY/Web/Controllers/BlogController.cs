using Microsoft.AspNetCore.Mvc;
using Web.Services.Abstract;
using Web.ViewModels.Blog;

namespace Web.Controllers
{


    public class BlogController : Controller
    {


        private readonly IBlogService _blogService;

        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(BlogIndexVM model)
        {
            model = await _blogService.GetAllAsync(model);
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var model = await _blogService.GetAsync(id);
            return View(model);
        }


    }
}
