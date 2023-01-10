using Web.ViewModels.About;

namespace Web.Services.Abstract
{
    public interface IAboutService
    {

        Task<AboutIndexVM> GetAllAsync();
    }
}

