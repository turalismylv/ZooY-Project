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

        public ShopService(IProductRepository productRepository,
            IActionContextAccessor actionContextAccessor, 
            IProductCategoryRepository productCategoryRepository,
            IBrandRepository brandRepository)
        {
            _modelState = actionContextAccessor.ActionContext.ModelState;
            _productRepository = productRepository;
            _productCategoryRepository = productCategoryRepository;
            _brandRepository = brandRepository;
        }


        //public async Task<ShopIndexVM> GetAllAsync(ShopIndexVM model)
        //{

        //    var products = FilterProducts(model);

        //    model = new ShopIndexVM
        //    {
        //        ProductCategories = await _productCategoryRepository.GetAllCategoryAsync(),
        //        Brands = await _brandRepository.GetAllBrandAsync(),
        //        Products = await products.ToListAsync()
        //    };
        //    return model;

        //}

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

        //public IQueryable<Product> FilterProducts(ShopIndexVM model)
        //{
        //    var products = _productRepository.FilterByTitle(model.Title);
        //    return products;
        //}


    }
}
