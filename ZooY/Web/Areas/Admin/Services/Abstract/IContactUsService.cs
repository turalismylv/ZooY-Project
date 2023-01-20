using Web.Areas.Admin.ViewModels.ContactUs;

namespace Web.Areas.Admin.Services.Abstract
{
    public interface IContactUsService
    {
        Task<ContactUsIndexVM> GetAllAsync();
        Task<ContactUsDetailsVM> GetDetailsModelAsync(int id);
        Task<bool> DeleteAsync(int id);
        Task<bool> ChangeStatus(int id);
    }
}
