using fiorello_project.Attributes;
using Microsoft.AspNetCore.Mvc;
using Web.Services.Abstract;
using Web.ViewModels.Account;

namespace Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [OnlyAnonymous]

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(AccountRegisterVM model)
        {
            var isSucceded = await _accountService.RegisterAsync(model);
            if (isSucceded)
            {
                string link = Url.Action(nameof(ConfirmEmail), "Account", new { userId = model.UserId, model.Token }, Request.Scheme, Request.Host.ToString());
                var isConfirmed = await _accountService.ConfirmationTokenEmailAsync(link, model);
                if (isConfirmed) return RedirectToAction("VerifyEmail", "Account");
            }
            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> VerifyEmail()
        {
            return View();
        }

        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            var isConfirmed = await _accountService.ConfirmEmailAsync(userId, token);
            if (isConfirmed) return RedirectToAction("index", "home");
            return BadRequest();
        }


        [OnlyAnonymous]

        [HttpGet]

        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(AccountLoginVM model)
        {
            var isSucceeded = await _accountService.LoginAsync(model);
            if (isSucceeded)
            {
                if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    return Redirect(model.ReturnUrl);
                else
                    return RedirectToAction("index", "home");
            }
            return View(model);

        }


        [HttpGet]

        public async Task<IActionResult> Logout()
        {
            await _accountService.Logout();


            return RedirectToAction(nameof(Login));

        }

    }
}