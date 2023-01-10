

using DataAccess.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Web.Services.Abstract;
using Web.ViewModels.About;

namespace Web.Services.Concrete
{
    public class AboutService : IAboutService
    {
        private readonly ModelStateDictionary _modelState;
        private readonly IWelcomeRepository _welcomeRepository;
        private readonly IOurHistoryRepository _ourHistoryRepository;
        private readonly ITestimonialRepository _testimonialRepository;

        public AboutService(IWelcomeRepository welcomeRepository,
            IOurHistoryRepository ourHistoryRepository,
            ITestimonialRepository testimonialRepository,
            IActionContextAccessor actionContextAccessor)
        {
            _modelState = actionContextAccessor.ActionContext.ModelState;
            _welcomeRepository = welcomeRepository;
            _ourHistoryRepository = ourHistoryRepository;
            _testimonialRepository = testimonialRepository;
        }


        public async Task<AboutIndexVM> GetAllAsync()
        {
            var model = new AboutIndexVM
            {
                Welcome = await _welcomeRepository.GetAsync(),
                OurHistory=await _ourHistoryRepository.GetAsync(),
                Testimonials=await _testimonialRepository.GetAllAsync()
            };
            return model;

        }
    }
}
