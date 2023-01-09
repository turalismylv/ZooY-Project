using Core.Entities;
using Core.Utilities.FileService;
using DataAccess.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.HotDeal;

namespace Web.Areas.Admin.Services.Concrete
{
    public class HotDealService : IHotDealService
    {

        private readonly IHotDealRepository _hotDealRepository;
        private readonly IFileService _fileService;
        private readonly ModelStateDictionary _modelState;

        public HotDealService(IHotDealRepository hotDealRepository, IActionContextAccessor actionContextAccessor, IFileService fileService)
        {
            _hotDealRepository = hotDealRepository;
            _fileService = fileService;
            _modelState = actionContextAccessor.ActionContext.ModelState;
        }


        public async Task<HotDealIndexVM> GetAsync()
        {
            var model = new HotDealIndexVM
            {
                HotDeal = await _hotDealRepository.GetAsync()
            };
            return model;
        }

        public async Task<bool> CreateAsync(HotDealCreateVM model)
        {
            if (!_modelState.IsValid) return false;

            if (!_fileService.IsImage(model.MainPhoto))
            {
                _modelState.AddModelError("MainPhoto", "File image formatinda deyil zehmet olmasa image formasinda secin!!");
                return false;
            }
            if (!_fileService.CheckSize(model.MainPhoto, 1000))
            {
                _modelState.AddModelError("MainPhoto", "File olcusu 300 kbdan boyukdur");
                return false;
            }

            var hotDeal = new HotDeal
            {
                Title = model.Title,
                Time = model.Time,
                Text = model.Text,
                CreatedAt = DateTime.Now,
                PhotoName = await _fileService.UploadAsync(model.MainPhoto),
            };

            await _hotDealRepository.CreateAsync(hotDeal);

            return true;
        }

        public async Task<HotDealUpdateVM> GetUpdateModelAsync(int id)
        {
            var hotDeal = await _hotDealRepository.GetAsync(id);

            if (hotDeal == null) return null;

            var model = new HotDealUpdateVM
            {
                Id = hotDeal.Id,
                Title = hotDeal.Title,
                MainPhotoPath = hotDeal.PhotoName,
                Text = hotDeal.Text,
                Time = hotDeal.Time,

            };

            return model;

        }


        public async Task<bool> UpdateAsync(HotDealUpdateVM model)
        {
            if (!_modelState.IsValid) return false;


            if (model.MainPhoto != null)
            {
                if (!_fileService.IsImage(model.MainPhoto))
                {
                    _modelState.AddModelError("MainPhoto", "File image formatinda deyil zehmet olmasa image formasinda secin!!");
                    return false;
                }
                if (!_fileService.CheckSize(model.MainPhoto, 1000))
                {
                    _modelState.AddModelError("MainPhoto", "File olcusu 300 kbdan boyukdur");
                    return false;
                }
            }

            var hotDeal = await _hotDealRepository.GetAsync(model.Id);

            if (hotDeal != null)
            {
                hotDeal.Title = model.Title;
                hotDeal.ModifiedAt = DateTime.Now;
                hotDeal.Text = model.Text;
                hotDeal.Time = model.Time;



                if (model.MainPhoto != null)
                {
                    hotDeal.PhotoName = await _fileService.UploadAsync(model.MainPhoto);
                }

                await _hotDealRepository.UpdateAsync(hotDeal);

            }
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var hotDeal = await _hotDealRepository.GetAsync(id);

            if (hotDeal != null)
            {
                _fileService.Delete(hotDeal.PhotoName);
                await _hotDealRepository.DeleteAsync(hotDeal);

                return true;

            }

            return false;
        }

    }
}
