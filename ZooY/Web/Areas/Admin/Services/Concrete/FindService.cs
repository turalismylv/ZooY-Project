using Core.Entities;
using Core.Utilities.FileService;
using DataAccess.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.Find;

namespace Web.Areas.Admin.Services.Concrete
{
    public class FindService :IFindService
    {

        private readonly IFindRepsitory _findRepsitory;
        private readonly IFileService _fileService;
        private readonly ModelStateDictionary _modelState;

        public FindService(IFindRepsitory findRepsitory, IActionContextAccessor actionContextAccessor, IFileService fileService)
        {
            _findRepsitory = findRepsitory;
            _fileService = fileService;
            _modelState = actionContextAccessor.ActionContext.ModelState;
        }


        public async Task<FindIndexVM> GetAsync()
        {
            var model = new FindIndexVM
            {
                Find = await _findRepsitory.GetAsync()
            };
            return model;
        }

        public async Task<bool> CreateAsync(FindCreateVM model)
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

            var find = new Find
            {
                Title = model.Title,
                CreatedAt = DateTime.Now,
                PhotoName = await _fileService.UploadAsync(model.MainPhoto),
            };

            await _findRepsitory.CreateAsync(find);

            return true;
        }

        public async Task<FindUpdateVM> GetUpdateModelAsync(int id)
        {
            var find = await _findRepsitory.GetAsync(id);

            if (find == null) return null;

            var model = new FindUpdateVM
            {
                Id = find.Id,
                Title = find.Title,
                MainPhotoPath = find.PhotoName,
               
            };

            return model;

        }


        public async Task<bool> UpdateAsync(FindUpdateVM model)
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

            var find = await _findRepsitory.GetAsync(model.Id);

            if (find != null)
            {
                find.Title = model.Title;
                find.ModifiedAt = DateTime.Now;
                


                if (model.MainPhoto != null)
                {
                    find.PhotoName = await _fileService.UploadAsync(model.MainPhoto);
                }

                await _findRepsitory.UpdateAsync(find);

            }
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var find = await _findRepsitory.GetAsync(id);

            if (find != null)
            {
                _fileService.Delete(find.PhotoName);
                await _findRepsitory.DeleteAsync(find);

                return true;

            }

            return false;
        }

    }
}
