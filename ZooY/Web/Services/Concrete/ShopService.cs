using Core.Entities;
using DataAccess.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Web.Services.Abstract;
using Web.ViewModels.Shop;

namespace Web.Services.Concrete
{
    public class ShopService :IShopService
    {

        private readonly ModelStateDictionary _modelState;
        private readonly IProductRepository _productRepository;
        private readonly IProductCategoryRepository _productCategoryRepository;
        private readonly IBrandRepository _brandRepository;
        private readonly ITagRepository _tagRepository;

        public ShopService(IProductRepository productRepository,
            IActionContextAccessor actionContextAccessor, 
            IProductCategoryRepository productCategoryRepository,
            IBrandRepository brandRepository,
            ITagRepository tagRepository)
        {
            _modelState = actionContextAccessor.ActionContext.ModelState;
            _productRepository = productRepository;
            _productCategoryRepository = productCategoryRepository;
            _brandRepository = brandRepository;
             _tagRepository = tagRepository;
        }


        public async Task<ShopIndexVM> GetAllAsync(ShopIndexVM model)
        {
            var pageCount = await _productRepository.GetPageCountAsync(model.Take, model.Title);

            if (model.Page <= 0 /*|| model.Page > pageCount*/) return model;

            var products = await _productRepository.Filter(model.Title, model.Page, model.Take);

            model = new ShopIndexVM
            {
                Products = products,
                Page = model.Page,
                PageCount = pageCount,
                Take = model.Take,
                Title = model.Title,
                 ProductCategories = await _productCategoryRepository.GetAllCategoryAsync(),
                 Brands = await _brandRepository.GetAllBrandAsync(),
                 Tags=await _tagRepository.GetAllAsync(),

            };
            return model;

        }

        public async Task<ShopDetailsVM> GetAsync(int id)
        {
            var product = await _productRepository.GetAsync(id);

            if (product == null) return null;

            var model = new ShopDetailsVM
            {
                Id = product.Id,
                Description = product.Description,
                PhotoName = product.PhotoName,
                Brand = await _brandRepository.GetAsync(product.BrandId),
                BrandId = product.BrandId,
                Price = product.Price,
                ProductCategory=await _productCategoryRepository.GetAsync(product.ProductCategoryId),
                Title = product.Title,
                ProductCategoryId = product.ProductCategoryId,
                

            
            };
            return model;

        }


        public async Task<ShopProductIndexVM> CategoryProductAsync(int id)
        {
            var category = await _productCategoryRepository.GetAsync(id);
            var model = new ShopProductIndexVM
            {
                ProductCategory = category,
                
                Products = category != null ? await _productRepository.GetByCategoryIdAsync(category.Id) : new List<Product>()
            };

            return model;

        }

        public async Task<ShopBrandProductIndexVM> BrandProductAsync(int id)
        {
            var brand = await _brandRepository.GetAsync(id);
            var model = new ShopBrandProductIndexVM
            {
                Brand = brand,

                Products = brand != null ? await _productRepository.GetByBrandIdAsync(brand.Id) : new List<Product>()
            };

            return model;

        }
        public async Task<ShopTagProductIndexVM> TagProductAsync(int id)
        {
            var tag = await _tagRepository.GetWithProductsAsync(id);
            if (tag == null) return null;

            var model = new ShopTagProductIndexVM
            {
                Tag = tag
            };
            return model;
        }



    }
}
