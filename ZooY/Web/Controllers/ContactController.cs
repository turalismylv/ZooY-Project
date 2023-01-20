using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Web.Services.Abstract;
using Web.ViewModels.Contact;

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


        [HttpPost]
        public async Task<IActionResult> Index(ContactIndexVM model)
        {
            var isSucceeded = await _contactService.CreateAsync(model);
            if (isSucceeded) return RedirectToAction(nameof(Index));
            return View(model);
        }

    }
}
