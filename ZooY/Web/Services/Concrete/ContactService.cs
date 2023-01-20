

using Core.Entities;
using DataAccess.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Web.Services.Abstract;
using Web.ViewModels.Contact;

namespace Web.Services.Concrete
{
    public class ContactService : IContactService
    {
        private readonly ModelStateDictionary _modelState;
        private readonly IContactInfoRepository _contactInfoRepository;
        private readonly IContactUsRepository _contactUsRepository;

        public ContactService(IContactInfoRepository contactInfoRepository,
           IContactUsRepository contactUsRepository,
            IActionContextAccessor actionContextAccessor)
        {
            _modelState = actionContextAccessor.ActionContext.ModelState;
            _contactInfoRepository = contactInfoRepository;
            _contactUsRepository = contactUsRepository;
        }


        public async Task<ContactIndexVM> GetAllAsync()
        {
            var model = new ContactIndexVM
            {
                ContactInfo = await _contactInfoRepository.GetAsync()
            };
            return model;

        }


        public async Task<bool> CreateAsync(ContactIndexVM model)
        {
            if (model.Status==null)
            {
                model.Status = 0;
            }
            if (!_modelState.IsValid) return false;

            var contactUs = new ContactUs
            {
                Subject = model.Subject,
                Email = model.Email,
                FullName = model.FullName,
                Message = model.Message,
                CreatedAt = DateTime.Now
               
            };

            await _contactUsRepository.CreateAsync(contactUs);

            return true;
        }

        
    }
}
