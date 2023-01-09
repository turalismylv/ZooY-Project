using Web.Areas.Admin.ViewModels.HomeMainSlider;

namespace Web.Areas.Admin.Services.Abstract
{
    public interface IHomeMainSliderService
    {
        Task<HomeMainSliderIndexVM> GetAllAsync();

        Task<bool> CreateAsync(HomeMainSliderCreateVM model);

        Task<HomeMainSliderUpdateVM> GetUpdateModelAsync(int id);

        Task<bool> UpdateAsync(HomeMainSliderUpdateVM model);

        Task<bool> DeleteAsync(int id);
    }
}
