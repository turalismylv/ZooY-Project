using Core.Entities;
using Core.Utilities.FileService;
using DataAccess.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.HomeMainSlider;

namespace Web.Areas.Admin.Services.Concrete
{
    public class HomeMainSliderService : IHomeMainSliderService
    {
        private readonly IHomeMainSliderRepository _homeMainSliderRepository;
        private readonly IFileService _fileService;
        private readonly ModelStateDictionary _modelState;

        public HomeMainSliderService(IHomeMainSliderRepository homeMainSliderRepository, IActionContextAccessor actionContextAccessor, IFileService fileService)
        {
            _homeMainSliderRepository = homeMainSliderRepository;

            _fileService = fileService;
            _modelState = actionContextAccessor.ActionContext.ModelState;
        }

        #region HomeMainSliderCrud

        public async Task<HomeMainSliderIndexVM> GetAllAsync()
        {
            var model = new HomeMainSliderIndexVM
            {
                HomeMainSliders = await _homeMainSliderRepository.GetAllAsync()
            };
            return model;

        }

        public async Task<bool> CreateAsync(HomeMainSliderCreateVM model)
        {
            if (!_modelState.IsValid) return false;

            var isExist = await _homeMainSliderRepository.AnyAsync(c => c.Title.Trim().ToLower() == model.Title.Trim().ToLower());
            if (isExist)
            {
                _modelState.AddModelError("Title", "Bu adda slider mövcuddur");
                return false;
            }

            if (!_fileService.IsImage(model.MainPhoto))
            {
                _modelState.AddModelError("MainPhoto", "File image formatinda deyil zehmet olmasa image formasinda secin!!");
                return false;
            }
            if (!_fileService.CheckSize(model.MainPhoto, 1000))
            {
                _modelState.AddModelError("MainPhoto", "File olcusu 1000 kbdan boyukdur");
                return false;
            }



            var homeMainSlider = new HomeMainSlider
            {
                Id = model.Id,
                Title = model.Title,
                Description = model.Description,
                SubDescription = model.SubDescription,
                CreatedAt = DateTime.Now,
                LearnUrl = model.LearnUrl,
                PhotoName = await _fileService.UploadAsync(model.MainPhoto),
            };

            await _homeMainSliderRepository.CreateAsync(homeMainSlider);
            return true;
        }


        public async Task<HomeMainSliderUpdateVM> GetUpdateModelAsync(int id)
        {


            var homeMainSlider = await _homeMainSliderRepository.GetAsync(id);

            if (homeMainSlider == null) return null;

            var model = new HomeMainSliderUpdateVM
            {
                Id = homeMainSlider.Id,
                Title = homeMainSlider.Title,
                Description = homeMainSlider.Description,
                SubDescription= homeMainSlider.SubDescription,
                MainPhotoName = homeMainSlider.PhotoName,
                LearnUrl = homeMainSlider.LearnUrl,
            };

            return model;

        }


        public async Task<bool> UpdateAsync(HomeMainSliderUpdateVM model)
        {
            if (!_modelState.IsValid) return false;

            var isExist = await _homeMainSliderRepository.AnyAsync(c => c.Title.Trim().ToLower() == model.Title.Trim().ToLower() && c.Id != model.Id);
            if (isExist)
            {
                _modelState.AddModelError("Title", "Bu adda kateqoriya mövcuddur");
                return false;
            }
            if (model.MainPhoto != null)
            {
                if (!_fileService.IsImage(model.MainPhoto))
                {
                    _modelState.AddModelError("MainPhoto", "File image formatinda deyil zehmet olmasa image formasinda secin!!");
                    return false;
                }
                if (!_fileService.CheckSize(model.MainPhoto, 1000))
                {
                    _modelState.AddModelError("MainPhoto", "File olcusu 1000 kbdan boyukdur");
                    return false;
                }
            }

            var homeMainSlider = await _homeMainSliderRepository.GetAsync(model.Id);




            if (homeMainSlider != null)
            {
                homeMainSlider.Id = model.Id;
                homeMainSlider.Title = model.Title;
                homeMainSlider.ModifiedAt = DateTime.Now;
                homeMainSlider.Description = model.Description;
                homeMainSlider.SubDescription = model.SubDescription;
                homeMainSlider.LearnUrl = model.LearnUrl;


                if (model.MainPhoto != null)
                {
                    homeMainSlider.PhotoName = await _fileService.UploadAsync(model.MainPhoto);
                }

                await _homeMainSliderRepository.UpdateAsync(homeMainSlider);

            }
            return true;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var homeMainSlider = await _homeMainSliderRepository.GetAsync(id);
            if (homeMainSlider != null)
            {
                _fileService.Delete(homeMainSlider.PhotoName);




                await _homeMainSliderRepository.DeleteAsync(homeMainSlider);

                return true;

            }

            return false;
        }



        #endregion

    }
}
