using Core.Entities;
using Web.ViewModels.Shop;

namespace Web.Services.Abstract
{
    public interface IShopService
    {
        Task<ShopIndexVM> GetAllAsync(ShopIndexVM model);
        Task<ShopProductIndexVM> CategoryProductAsync(int id);
        Task<ShopBrandProductIndexVM> BrandProductAsync(int id);
        //IQueryable<Product> FilterProducts(ShopIndexVM model);
    }
}
