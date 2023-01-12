using Core.Entities;
using Core.Utilities.FileService;
using DataAccess.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.ContactInfo;
using Web.Areas.Admin.ViewModels.HomeMainSlider;

namespace Web.Areas.Admin.Services.Concrete
{
    public class ContactInfoService : IContactInfoService
    {
        private readonly IContactInfoRepository _contactInfoRepository;
        private readonly ModelStateDictionary _modelState;

        public ContactInfoService(IContactInfoRepository contactInfoRepository, IActionContextAccessor actionContextAccessor)
        {
            _contactInfoRepository = contactInfoRepository;
            _modelState = actionContextAccessor.ActionContext.ModelState;
        }

        #region ContactInfo

        public async Task<ContactInfoIndexVM> GetAsync()
        {
            var model = new ContactInfoIndexVM
            {
                ContactInfo = await _contactInfoRepository.GetAsync()
            };
            return model;
        }

        public async Task<bool> CreateAsync(ContactInfoCreateVM model)
        {
            if (!_modelState.IsValid) return false;

            var contactInfo = new ContactInfo
            {
                Address = model.Address,
                Email = model.Email,
                CreatedAt = DateTime.Now,
                Phone = model.Phone,
                Website = model.Website,

            };

            await _contactInfoRepository.CreateAsync(contactInfo);

            return true;
        }

        public async Task<ContactInfoUpdateVM> GetUpdateModelAsync(int id)
        {
            var contactInfo = await _contactInfoRepository.GetAsync(id);

            if (contactInfo == null) return null;

            var model = new ContactInfoUpdateVM
            {
                Id = contactInfo.Id,
                Address = contactInfo.Address,
                Email = contactInfo.Email,
                Phone = contactInfo.Phone,
                Website = contactInfo.Website,

            };

            return model;

        }


        public async Task<bool> UpdateAsync(ContactInfoUpdateVM model)
        {
            if (!_modelState.IsValid) return false;

            var contactInfo = await _contactInfoRepository.GetAsync(model.Id);

            if (contactInfo != null)
            {
                contactInfo.Address = model.Address;
                contactInfo.Phone = model.Phone;
                contactInfo.Email = model.Email;
                contactInfo.Website = model.Website;
                contactInfo.ModifiedAt = DateTime.Now;

                await _contactInfoRepository.UpdateAsync(contactInfo);

            }
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var contactInfo = await _contactInfoRepository.GetAsync(id);

            if (contactInfo != null)
            {
                await _contactInfoRepository.DeleteAsync(contactInfo);

                return true;
            }
            return false;
        }
        #endregion

    }
}
