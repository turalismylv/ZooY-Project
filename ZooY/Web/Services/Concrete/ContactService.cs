

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

        public ContactService(IContactInfoRepository contactInfoRepository,
           
            IActionContextAccessor actionContextAccessor)
        {
            _modelState = actionContextAccessor.ActionContext.ModelState;
            _contactInfoRepository = contactInfoRepository;
        }


        public async Task<ContactIndexVM> GetAllAsync()
        {
            var model = new ContactIndexVM
            {
                ContactInfo=await _contactInfoRepository.GetAsync()
            };
            return model;

        }
    }
}
