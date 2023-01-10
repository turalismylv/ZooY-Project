using Core.Entities;
using Core.Utilities.FileService;
using DataAccess.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.ProductCategory;

namespace Web.Areas.Admin.Services.Concrete
{
    public class ProductCategoryService : IProductCategoryService
    {
        private readonly IProductCategoryRepository _productCategoryRepository;
        private readonly IFileService _fileService;
        private readonly ModelStateDictionary _modelState;

        public ProductCategoryService(IProductCategoryRepository productCategoryRepository, IActionContextAccessor actionContextAccessor, IFileService fileService)
        {
            _productCategoryRepository = productCategoryRepository;
            _fileService = fileService;
            _modelState = actionContextAccessor.ActionContext.ModelState;
        }

        #region HomeMainSliderCrud

        public async Task<ProductCategoryIndexVM> GetAllAsync()
        {
            var model = new ProductCategoryIndexVM
            {
                ProductCategories = await _productCategoryRepository.GetAllAsync()
            };
            return model;

        }

        public async Task<bool> CreateAsync(ProductCategoryCreateVM model)
        {
            if (!_modelState.IsValid) return false;

            var isExist = await _productCategoryRepository.AnyAsync(c => c.Title.Trim().ToLower() == model.Title.Trim().ToLower());
            if (isExist)
            {
                _modelState.AddModelError("Title", "Bu adda category mövcuddur");
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



            var productCategory = new ProductCategory
            {
                Id = model.Id,
                Title = model.Title,
                CreatedAt = DateTime.Now,
                PhotoName = await _fileService.UploadAsync(model.MainPhoto),
            };

            await _productCategoryRepository.CreateAsync(productCategory);
            return true;
        }


        public async Task<ProductCategoryUpdateVM> GetUpdateModelAsync(int id)
        {


            var productCategory = await _productCategoryRepository.GetAsync(id);

            if (productCategory == null) return null;

            var model = new ProductCategoryUpdateVM
            {
                Id = productCategory.Id,
                Title = productCategory.Title,
                MainPhotoName = productCategory.PhotoName,
            };

            return model;

        }


        public async Task<bool> UpdateAsync(ProductCategoryUpdateVM model)
        {
            if (!_modelState.IsValid) return false;

            var isExist = await _productCategoryRepository.AnyAsync(c => c.Title.Trim().ToLower() == model.Title.Trim().ToLower() && c.Id != model.Id);
            if (isExist)
            {
                _modelState.AddModelError("Title", "Bu adda Category mövcuddur");
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

            var productCategory = await _productCategoryRepository.GetAsync(model.Id);




            if (productCategory != null)
            {
                productCategory.Id = model.Id;
                productCategory.Title = model.Title;
                productCategory.ModifiedAt = DateTime.Now;


                if (model.MainPhoto != null)
                {
                    productCategory.PhotoName = await _fileService.UploadAsync(model.MainPhoto);
                }

                await _productCategoryRepository.UpdateAsync(productCategory);

            }
            return true;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var productCategory = await _productCategoryRepository.GetAsync(id);
            if (productCategory != null)
            {
                _fileService.Delete(productCategory.PhotoName);




                await _productCategoryRepository.DeleteAsync(productCategory);

                return true;

            }

            return false;
        }



        #endregion

    }
}
