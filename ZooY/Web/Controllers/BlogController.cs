using Microsoft.AspNetCore.Mvc;
using Web.Services.Abstract;

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
        public async Task<IActionResult> Index()
        {
            var model = await _blogService.GetAllAsync();
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
