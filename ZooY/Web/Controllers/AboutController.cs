using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Web.Services.Abstract;

namespace Web.Controllers
{

    public class AboutController : Controller
    {


        private readonly IAboutService _aboutService;

        public AboutController(IAboutService aboutService)
        {
            _aboutService = aboutService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = await _aboutService.GetAllAsync();
            return View(model);
        }


    }
}
