using Core.Entities;
using Core.Utilities.FileService;
using DataAccess.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.OurHistory;

namespace Web.Areas.Admin.Services.Concrete
{
    public class OurHistoryService : IOurHistoryService
    {
        private readonly IOurHistoryRepository _ourHistoryRepository;
        private readonly IFileService _fileService;
        private readonly ModelStateDictionary _modelState;

        public OurHistoryService(IOurHistoryRepository ourHistoryRepository, IActionContextAccessor actionContextAccessor, IFileService fileService)
        {
            _ourHistoryRepository = ourHistoryRepository;
            _fileService = fileService;
            _modelState = actionContextAccessor.ActionContext.ModelState;
        }


        public async Task<OurHistoryIndexVM> GetAsync()
        {
            var model = new OurHistoryIndexVM
            {
                OurHistory = await _ourHistoryRepository.GetAsync()
            };
            return model;
        }

        public async Task<bool> CreateAsync(OurHistoryCreateVM model)
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

            var ourHistory = new OurHistory
            {
                Title = model.Title,
                CreatedAt = DateTime.Now,
                PhotoName = await _fileService.UploadAsync(model.MainPhoto),
                Description = model.Description,
                SubTitle = model.SubTitle,
            };

            await _ourHistoryRepository.CreateAsync(ourHistory);

            return true;
        }

        public async Task<OurHistoryUpdateVM> GetUpdateModelAsync(int id)
        {
            var ourHistory = await _ourHistoryRepository.GetAsync(id);

            if (ourHistory == null) return null;

            var model = new OurHistoryUpdateVM
            {
                Id = ourHistory.Id,
                Title = ourHistory.Title,
                MainPhotoPath = ourHistory.PhotoName,
                Description = ourHistory.Description,
                SubTitle=ourHistory.SubTitle,
            };

            return model;

        }


        public async Task<bool> UpdateAsync(OurHistoryUpdateVM model)
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

            var ourHistory = await _ourHistoryRepository.GetAsync(model.Id);

            if (ourHistory != null)
            {
                ourHistory.Title = model.Title;
                ourHistory.ModifiedAt = DateTime.Now;
                ourHistory.Description = model.Description;
                ourHistory.SubTitle = model.SubTitle;



                if (model.MainPhoto != null)
                {
                    ourHistory.PhotoName = await _fileService.UploadAsync(model.MainPhoto);
                }

                await _ourHistoryRepository.UpdateAsync(ourHistory);

            }
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var welcome = await _ourHistoryRepository.GetAsync(id);

            if (welcome != null)
            {
                _fileService.Delete(welcome.PhotoName);
                await _ourHistoryRepository.DeleteAsync(welcome);
                return true;
            }
            return false;
        }

    }
}
