﻿

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
        private readonly IBrandRepository _brandRepository;
        private readonly IProductCategoryRepository _productCategoryRepository;
        private readonly ModelStateDictionary _modelState;

        public HomeService( IHomeMainSliderRepository homeMainSliderRepository,
            IFindRepsitory findRepsitory,
            IHotDealRepository hotDealRepository,
            IBrandRepository brandRepository,
            IProductCategoryRepository productCategoryRepository,
            IActionContextAccessor actionContextAccessor)
        {
            _homeMainSliderRepository = homeMainSliderRepository;
            _findRepsitory = findRepsitory;
            _hotDealRepository = hotDealRepository;
            _brandRepository = brandRepository;
            _productCategoryRepository = productCategoryRepository;
            _modelState = actionContextAccessor.ActionContext.ModelState;
        }


        public async Task<HomeIndexVM> GetAllAsync()
        {
            var model = new HomeIndexVM
            {
                HomeMainSliders = await _homeMainSliderRepository.GetAllAsync(),
                Find=await _findRepsitory.GetAsync(),
                HotDeal=await _hotDealRepository.GetAsync(),
                Brands=await _brandRepository.GetAllAsync(),
                ProductCategories=await _productCategoryRepository.GetAllAsync(),
            };
            return model;

        }
    }
}
