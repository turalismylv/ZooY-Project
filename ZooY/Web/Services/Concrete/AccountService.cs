using Core.Entities;
using Core.Utilities.FileService;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Web.Services.Abstract;
using Web.ViewModels.Account;

namespace Web.Services.Concrete
{
    public class AccountService :IAccountService
    {
        private readonly UserManager<User> _userManager; //user yaratmaq ucundur
        private readonly SignInManager<User> _signInManager; // userin login olmasi ucundur
        private readonly IEmailService _emailService;
        private readonly IFileService _fileService;
        private readonly ModelStateDictionary _modelState;

        public AccountService(UserManager<User> userManager,
            SignInManager<User> signInManager,
            IActionContextAccessor actionContextAccessor,
            IEmailService emailService,
            IFileService fileService
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
            _fileService = fileService;
            _modelState = actionContextAccessor.ActionContext.ModelState;
        }

        public async Task<bool> RegisterAsync(AccountRegisterVM model)
        {
            if (!_modelState.IsValid) return false;

            var isExistUserName = _userManager.Users.FirstOrDefault(u => u.UserName == model.Username);
            if (isExistUserName != null)
            {
                _modelState.AddModelError("UserName", $"{model.Username} artiq movcuddur");
                return false;
            }

            var isExistEmail = _userManager.Users.FirstOrDefault(u => u.Email == model.Email);
            if (isExistEmail != null)
            {
                _modelState.AddModelError("Email", $"{model.Email} has already taken");
                return false;
            }

            var user = new User
            {
            
                Email = model.Email,
                UserName = model.Username
            };

            model.UserId = user.Id;

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    _modelState.AddModelError(string.Empty, error.Description);
                }

                return false;
            }


            string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            model.Token = token;


            return true;
        }

        public async Task<bool> LoginAsync(AccountLoginVM model)
        {
            if (!_modelState.IsValid) return false;

            var user = await _userManager.FindByNameAsync(model.Username);
            if (user == null)
            {
                _modelState.AddModelError(string.Empty, "Username or Password is incorrect");
                return false;
            }

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);

            if (!result.Succeeded)
            {
                _modelState.AddModelError(string.Empty, "Username or Password is incorrect");
                return false;
            }

            return true;
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
            
        }

        public async Task<bool> ConfirmEmailAsync(string userId, string token)
        {
            if (userId == null || token == null) return false;

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null) return false;

            await _userManager.ConfirmEmailAsync(user, token);

            await _signInManager.SignInAsync(user, false);

            return true;
        }

        public async Task<bool> ConfirmationTokenEmailAsync(string link, AccountRegisterVM model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);

            string path = "wwwroot/templates/verify.html";
            string body = string.Empty;
            string subject = "Verify Email";

            body = _fileService.ReadFile(path, body);

            body = body.Replace("{{link}}", link);

            body = body.Replace("{{username}}", user.UserName.ToUpper());

            _emailService.Send(user.Email, subject, body);
            return true;
        }

        public async Task<bool> ResetPasswordTokenAsync(string link, ForgotPasswordVM model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            string path = "wwwroot/templates/resetpasswordverify.html";
            string body = string.Empty;
            string subject = "Verify Password Reset";

            body = _fileService.ReadFile(path, body);

            body = body.Replace("{{link}}", link);

            body = body.Replace("{{username}}", user.UserName);

            _emailService.Send(user.Email, subject, body);
            return true;
        }
        public async Task<bool> ResetPasswordAsync(ResetPasswordVM resetPasswordVM)
        {
            if (!_modelState.IsValid)
            {
                _modelState.AddModelError("Password", "Password is Required");
                return false;
            }


            var existUser = await _userManager.FindByIdAsync(resetPasswordVM.Id);
            if (existUser == null)
            {
                _modelState.AddModelError("Password", "User cannot found");
                return false;
            }

            if (await _userManager.CheckPasswordAsync(existUser, resetPasswordVM.Password))
            {
                _modelState.AddModelError("Password", "New password cant be same with old password");
                return false;
            }
            await _userManager.ResetPasswordAsync(existUser, resetPasswordVM.Token, resetPasswordVM.Password);
            return true;
        }

        public async Task<bool> ForgotPasswordFindUserAsync(ForgotPasswordVM forgotPasswordVM)
        {
            if (!_modelState.IsValid) return false;

            var userExist = await _userManager.FindByEmailAsync(forgotPasswordVM.Email);
            if (userExist == null)
            {
                _modelState.AddModelError("Email", "User Not Found");
                return false;
            }

            var resetVM = new ResetPasswordVM
            {
                Id = userExist.Id,
            };



            string token = await _userManager.GeneratePasswordResetTokenAsync(userExist);

            forgotPasswordVM.Token = token;
            forgotPasswordVM.Id = userExist.Id;

            return true;

        }

    }
}
