using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Web.Services.Abstract;

namespace Web.Controllers
{

    public class ContactController : Controller
    {


        private readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {
           _contactService = contactService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = await _contactService.GetAllAsync();
            return View(model);
        }


    }
}
