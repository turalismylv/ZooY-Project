using Web.Areas.Admin.ViewModels.HotDeal;

namespace Web.Areas.Admin.Services.Abstract
{
    public interface IHotDealService
    {
        Task<HotDealIndexVM> GetAsync();
        Task<bool> CreateAsync(HotDealCreateVM model);

        Task<HotDealUpdateVM> GetUpdateModelAsync(int id);
        Task<bool> UpdateAsync(HotDealUpdateVM model);
        Task<bool> DeleteAsync(int id);
    }
}
