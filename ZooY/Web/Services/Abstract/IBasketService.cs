using Core.Entities;
using System.Security.Claims;
using Web.ViewModels.Basket;

namespace Web.Services.Abstract
{
    public interface IBasketService
    {
        Task<BasketIndexVM> GetAsync();

        Task<bool> Add(BasketAddVM model);
        Task<bool> DeleteBasketProduct(int productId);

        Task<bool> UpCount(int productId);
        Task<bool> DownCount(int productId);

        Task<bool> ClearBasketProduct();

        
    }
}
