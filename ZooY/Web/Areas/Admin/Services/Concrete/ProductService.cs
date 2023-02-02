using Core.Entities;
using Core.Utilities.FileService;
using DataAccess.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.Product;

namespace Web.Areas.Admin.Services.Concrete
{
    public class ProductService :IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IBrandRepository _brandRepository;
        private readonly IActionContextAccessor actionContextAccessor;
        private readonly IFileService _fileService;
        private readonly IProductCategoryRepository _productCategoryRepository;
        private readonly ITagRepository _tagRepository;
        private readonly IProductTagRepository _productTagRepository;
        private readonly ModelStateDictionary _modelState;
        public ProductService(IProductRepository productRepository,
            IBrandRepository brandRepository,
            IActionContextAccessor actionContextAccessor,
            IFileService fileService,
            IProductCategoryRepository productCategoryRepository,
            ITagRepository tagRepository,
            IProductTagRepository productTagRepository)
        {
            _productRepository = productRepository;
            _brandRepository = brandRepository;
            _fileService = fileService;
            _productCategoryRepository = productCategoryRepository;
            _tagRepository = tagRepository;
            _productTagRepository = productTagRepository;
            _modelState = actionContextAccessor.ActionContext.ModelState;
        }

        public async Task<ProductIndexVM> GetAllAsync()
        {
            var model = new ProductIndexVM
            {
                Products = await _productRepository.GetAllGetCategoryAndGetBrandAsync()
            };
            return model;

        }
        public async Task<ProductCreateVM> GetCreateModelAsync()
        {
            var categories = await _productCategoryRepository.GetAllAsync();
            var brands = await _brandRepository.GetAllAsync();
            var model = new ProductCreateVM
            {
                Categories = categories.Select(c => new SelectListItem
                {
                    Text = c.Title,
                    Value = c.Id.ToString()
                }).ToList(),

                 Brands = brands.Select(c => new SelectListItem
                 {
                     Text = c.Title,
                     Value = c.Id.ToString()
                 }).ToList()
            };

            return model;
        }


        public async Task<ProductDetailsVM> GetDetailsModelAsync(int id)
        {
            var product = await _productRepository.GetWithTagsAsync(id);
            if (product == null) return null;

            var model = new ProductDetailsVM
            {
                Product = product,
                Brand=await _brandRepository.GetAsync(product.BrandId),
                ProductCategory=await _productCategoryRepository.GetAsync(product.ProductCategoryId),
                
            };

            return model;
        }

        public async Task<bool> CreateAsync(ProductCreateVM model)
        {
            if (!_modelState.IsValid) return false;

            var isExist = await _productRepository.AnyAsync(c => c.Title.Trim().ToLower() == model.Title.Trim().ToLower());
            if (isExist)
            {
                _modelState.AddModelError("Title", "Bu adda Product mövcuddur");
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



            var product = new Product
            {
                Title = model.Title,
                Description = model.Description,
                CreatedAt = DateTime.Now,
                ProductCategoryId = model.CategoryId,
                BrandId = model.BrandId,
                Price = model.Price,
                HomePageSee = model.HomePageSee,
                PhotoName = await _fileService.UploadAsync(model.MainPhoto)


            };

            await _productRepository.CreateAsync(product);

            return true;
        }

        public async Task<ProductUpdateVM> GetUpdateModelAsync(int id)
        {


            var categories = await _productCategoryRepository.GetAllAsync();

            var brands = await _brandRepository.GetAllAsync();

            var product = await _productRepository.GetAsync(id);

            if (product == null) return null;

            var model = new ProductUpdateVM
            {
                Id = product.Id,
                Categories = categories.Select(c => new SelectListItem
                {
                    Text = c.Title,
                    Value = c.Id.ToString()
                }).ToList(),
                Brands = brands.Select(c => new SelectListItem
                {
                    Text = c.Title,
                    Value = c.Id.ToString()
                }).ToList(),

                CategoryId = product.ProductCategoryId,
                BrandId=product.BrandId,
                Price = product.Price,
                Title = product.Title,
                Description = product.Description,
                MainPhotoPath = product.PhotoName,
                HomePageSee=product.HomePageSee,

            };

            return model;

        }

        public async Task<bool> UpdateAsync(ProductUpdateVM model)
        {
            if (!_modelState.IsValid) return false;

            var isExist = await _productRepository.AnyAsync(c => c.Title.Trim().ToLower() == model.Title.Trim().ToLower() && c.Id != model.Id);
            if (isExist)
            {
                _modelState.AddModelError("Title", "Bu adda Product mövcuddur");
                return false;
            }
            if (model.MainPhoto != null)
            {
                if (!_fileService.IsImage(model.MainPhoto))
                {
                    _modelState.AddModelError("MainPhoto", "File image formatinda deyil zehmet olmasa image formasinda secin!!");
                    return false;
                }
                if (!_fileService.CheckSize(model.MainPhoto, 300))
                {
                    _modelState.AddModelError("MainPhoto", "File olcusu 300 kbdan boyukdur");
                    return false;
                }
            }

            var product = await _productRepository.GetAsync(model.Id);
            if (product != null)
            {
                product.Title = model.Title;
                product.Description = model.Description;
                product.ModifiedAt = DateTime.Now;
                product.Price = model.Price;
                product.ProductCategoryId = model.CategoryId;
                product.BrandId = model.BrandId;
                product.HomePageSee=model.HomePageSee;

                if (model.MainPhoto != null)
                {
                    product.PhotoName = await _fileService.UploadAsync(model.MainPhoto);
                }
                await _productRepository.UpdateAsync(product);
            }
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var product = await _productRepository.GetAsync(id);
            if (product != null)
            {
                _fileService.Delete(product.PhotoName);
                await _productRepository.DeleteAsync(product);

                return true;
            }
            return false;
        }

        public async Task<bool> AddTagsAsync(ProductAddTagsVM model)
        {
            if (!_modelState.IsValid) return false;

            var product = await _productRepository.GetAsync(model.ProductId);
            if (product == null)
            {
                _modelState.AddModelError("ProductId", "Product tapılmadı");
                return false;
            }

            foreach (var tagId in model.TagsIds)
            {
                var tag = await _tagRepository.GetAsync(tagId);
                if (tag == null)
                {
                    _modelState.AddModelError(string.Empty, $"{tagId} id-li tag tapılmadı");
                    return false;
                }

                var isExist = await _productTagRepository.AnyAsync(ct => ct.ProductId == product.Id && ct.TagId == tagId);
                if (isExist)
                {
                    _modelState.AddModelError(string.Empty, $"{tag.Id} id-li tag artıq bu Product əlavə olunub");
                    return false;
                }

                var productTag = new ProductTag
                {
                    ProductId = product.Id,
                    TagId = tag.Id
                };

                await _productTagRepository.CreateAsync(productTag);
            }

            return true;
        }

        public async Task<ProductAddTagsVM> GetAddTagsModelAsync(int id)
        {
            var product = await _productRepository.GetAsync(id);
            if (product == null) return null;

            var tags = await _tagRepository.GetAllAsync();

            var model = new ProductAddTagsVM
            {
                Tags = tags.Select(t => new SelectListItem
                {
                    Text = t.Title,
                    Value = t.Id.ToString()
                })
                .ToList()
            };

            return model;
        }
    }
}
