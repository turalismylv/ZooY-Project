using Web.Areas.Admin.ViewModels.Welcome;

namespace Web.Areas.Admin.Services.Abstract
{
    public interface IWelcomeService
    {
        Task<WelcomeIndexVM> GetAsync();
        Task<bool> CreateAsync(WelcomeCreateVM model);

        Task<WelcomeUpdateVM> GetUpdateModelAsync(int id);
        Task<bool> UpdateAsync(WelcomeUpdateVM model);
        Task<bool> DeleteAsync(int id);
    }
}