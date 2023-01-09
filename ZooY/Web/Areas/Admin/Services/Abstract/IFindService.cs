using Web.Areas.Admin.ViewModels.Find;

namespace Web.Areas.Admin.Services.Abstract
{
    public interface IFindService
    {
        Task<FindIndexVM> GetAsync();
        Task<bool> CreateAsync(FindCreateVM model);

        Task<FindUpdateVM> GetUpdateModelAsync(int id);
        Task<bool> UpdateAsync(FindUpdateVM model);
        Task<bool> DeleteAsync(int id);
    }
}
