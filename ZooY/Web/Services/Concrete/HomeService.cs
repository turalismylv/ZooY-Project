

using DataAccess.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Web.Services.Abstract;
using Web.ViewModels.Home;

namespace Web.Services.Concrete
{
    public class HomeService : IHomeService
    {
        private readonly IHomeMainSliderRepository _homeMainSliderRepository;
        private readonly IFindRepsitory _findRepsitory;
        private readonly IHotDealRepository _hotDealRepository;
        private readonly ModelStateDictionary _modelState;

        public HomeService( IHomeMainSliderRepository homeMainSliderRepository,
            IFindRepsitory findRepsitory,
            IHotDealRepository hotDealRepository,
            IActionContextAccessor actionContextAccessor)
        {
            _homeMainSliderRepository = homeMainSliderRepository;
            _findRepsitory = findRepsitory;
            _hotDealRepository = hotDealRepository;
            _modelState = actionContextAccessor.ActionContext.ModelState;
        }


        public async Task<HomeIndexVM> GetAllAsync()
        {
            var model = new HomeIndexVM
            {
                HomeMainSliders = await _homeMainSliderRepository.GetAllAsync(),
                Find=await _findRepsitory.GetAsync(),
                HotDeal=await _hotDealRepository.GetAsync(),
            };
            return model;

        }
    }
}
