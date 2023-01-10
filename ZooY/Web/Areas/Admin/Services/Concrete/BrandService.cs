using Core.Entities;
using Core.Utilities.FileService;
using DataAccess.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.Brand;

namespace Web.Areas.Admin.Services.Concrete
{
    public class BrandService : IBrandService
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IFileService _fileService;
        private readonly ModelStateDictionary _modelState;

        public BrandService(IBrandRepository brandRepository, IActionContextAccessor actionContextAccessor, IFileService fileService)
        {
            _brandRepository = brandRepository;
            _fileService = fileService;
            _modelState = actionContextAccessor.ActionContext.ModelState;
        }

        #region HomeMainSliderCrud

        public async Task<BrandIndexVM> GetAllAsync()
        {
            var model = new BrandIndexVM
            {
                Brands = await _brandRepository.GetAllAsync()
            };
            return model;

        }

        public async Task<bool> CreateAsync(BrandCreateVM model)
        {
            if (!_modelState.IsValid) return false;

            var isExist = await _brandRepository.AnyAsync(c => c.Title.Trim().ToLower() == model.Title.Trim().ToLower());
            if (isExist)
            {
                _modelState.AddModelError("Title", "Bu adda Brand mövcuddur");
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



            var brand = new Brand
            {
                Id = model.Id,
                Title = model.Title,
                CreatedAt = DateTime.Now,
                PhotoName = await _fileService.UploadAsync(model.MainPhoto),
            };

            await _brandRepository.CreateAsync(brand);
            return true;
        }


        public async Task<BrandUpdateVM> GetUpdateModelAsync(int id)
        {


            var brand = await _brandRepository.GetAsync(id);

            if (brand == null) return null;

            var model = new BrandUpdateVM
            {
                Id = brand.Id,
                Title = brand.Title,
                MainPhotoName = brand.PhotoName,
            };

            return model;

        }


        public async Task<bool> UpdateAsync(BrandUpdateVM model)
        {
            if (!_modelState.IsValid) return false;

            var isExist = await _brandRepository.AnyAsync(c => c.Title.Trim().ToLower() == model.Title.Trim().ToLower() && c.Id != model.Id);
            if (isExist)
            {
                _modelState.AddModelError("Title", "Bu adda brand mövcuddur");
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

            var brand = await _brandRepository.GetAsync(model.Id);




            if (brand != null)
            {
                brand.Id = model.Id;
                brand.Title = model.Title;
                brand.ModifiedAt = DateTime.Now;


                if (model.MainPhoto != null)
                {
                    brand.PhotoName = await _fileService.UploadAsync(model.MainPhoto);
                }

                await _brandRepository.UpdateAsync(brand);

            }
            return true;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var brand = await _brandRepository.GetAsync(id);
            if (brand != null)
            {
                _fileService.Delete(brand.PhotoName);




                await _brandRepository.DeleteAsync(brand);

                return true;

            }

            return false;
        }



        #endregion

    }
}
