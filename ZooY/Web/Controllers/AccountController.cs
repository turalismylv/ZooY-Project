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


        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }



        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordVM forgotPasswordVM)
        {
            var userIsExist = await _accountService.ForgotPasswordFindUserAsync(forgotPasswordVM);
            if (!userIsExist) return View(forgotPasswordVM);
            string link = Url.Action(nameof(ResetPassword), "Account", new { userId = forgotPasswordVM.Id, forgotPasswordVM.Token },
             Request.Scheme, Request.Host.ToString());
            await _accountService.ResetPasswordTokenAsync(link, forgotPasswordVM);

            return RedirectToAction(nameof(VerifyEmail));
        }



        [HttpGet]
        public async Task<IActionResult> VerifyEmail()
        {
            return View();
        }

    


        [HttpGet]
        public IActionResult ResetPassword(string userId, string token) => View(new ResetPasswordVM { Token = token, Id = userId });


        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordVM resetPasswordVM)
        {
            var isSucceded = await _accountService.ResetPasswordAsync(resetPasswordVM);
            if (isSucceded) return RedirectToAction(nameof(Login));
            return View(resetPasswordVM);

        }




        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            var isConfirmed = await _accountService.ConfirmEmailAsync(userId, token);
            if (isConfirmed) return RedirectToAction("index", "home");
            return BadRequest();
        }
    }
}