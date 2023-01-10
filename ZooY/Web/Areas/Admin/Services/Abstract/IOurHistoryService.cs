using Web.Areas.Admin.ViewModels.OurHistory;

namespace Web.Areas.Admin.Services.Abstract
{
    public interface IOurHistoryService
    {
        Task<OurHistoryIndexVM> GetAsync();
        Task<bool> CreateAsync(OurHistoryCreateVM model);

        Task<OurHistoryUpdateVM> GetUpdateModelAsync(int id);
        Task<bool> UpdateAsync(OurHistoryUpdateVM model);
        Task<bool> DeleteAsync(int id);
    }
}
