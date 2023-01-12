using Web.Areas.Admin.ViewModels.ContactInfo;

namespace Web.Areas.Admin.Services.Abstract
{
    public interface IContactInfoService
    {
        Task<ContactInfoIndexVM> GetAsync();

        Task<bool> CreateAsync(ContactInfoCreateVM model);

        Task<ContactInfoUpdateVM> GetUpdateModelAsync(int id);

        Task<bool> UpdateAsync(ContactInfoUpdateVM model);

        Task<bool> DeleteAsync(int id);
    }
}
