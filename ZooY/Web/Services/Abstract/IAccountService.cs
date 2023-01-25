﻿using Web.ViewModels.Account;

namespace Web.Services.Abstract
{
    public interface IAccountService
    {
        Task<bool> RegisterAsync(AccountRegisterVM model);

        Task<bool> LoginAsync(AccountLoginVM model);

        Task Logout();

        Task<bool> ConfirmEmailAsync(string userId, string token);

        Task<bool> ConfirmationTokenEmailAsync(string link, AccountRegisterVM model);

        Task<bool> ResetPasswordTokenAsync(string link, ForgotPasswordVM model);
        Task<bool> ResetPasswordAsync(ResetPasswordVM resetPasswordVM);
        Task<bool> ForgotPasswordFindUserAsync(ForgotPasswordVM forgotPasswordVM);
    }
}
