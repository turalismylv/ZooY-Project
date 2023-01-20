using DataAccess.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.ContactUs;

namespace Web.Areas.Admin.Services.Concrete
{
    public class ContactUsService:IContactUsService
    {
        private readonly ModelStateDictionary _modelState;
        private readonly IContactUsRepository _contactUsRepository;

        public ContactUsService(IContactUsRepository contactUsRepository, IActionContextAccessor actionContextAccessor)
        {
            _modelState = actionContextAccessor.ActionContext.ModelState;
            _contactUsRepository = contactUsRepository;
        }

        public async Task<ContactUsIndexVM> GetAllAsync()
        {
            var model = new ContactUsIndexVM
            {
                ContactUs = await _contactUsRepository.GetOrderContactUs()
            };
            return model;

        }
        public async Task<ContactUsDetailsVM> GetDetailsModelAsync(int id)
        {
            var contactUs = await _contactUsRepository.GetAsync(id);
            if (contactUs == null) return null;

            var model = new ContactUsDetailsVM
            {
               ContactUs= contactUs,

            };

            return model;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var contactUs = await _contactUsRepository.GetAsync(id);

            if (contactUs != null)
            {
                await _contactUsRepository.DeleteAsync(contactUs);

                return true;
            }
            return false;
        }

        public async Task<bool> ChangeStatus(int id)
        {
            var contactUs = await _contactUsRepository.GetAsync(id);

            if (contactUs != null)
            {
                contactUs.Status=Core.Constans.ContactStatus.Oxundu;
                contactUs.ModifiedAt = DateTime.Now;

               await _contactUsRepository.UpdateAsync(contactUs);

                return true;
            }
            return false;
        }
    }
}
