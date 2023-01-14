using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Abstract
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<List<Product>> GetAllGetCategoryAndGetBrandAsync();

        Task<List<Product>> GetByCategoryIdAsync(int categoryId);

        Task<List<Product>> GetByBrandIdAsync(int brandId);
        //IQueryable<Product> FilterByTitle(string title);
        Task<Product> GetProduct(int productId);

        Task<int> GetPageCountAsync(int take, string title);

        IQueryable<Product> FilterByTitle(string title);
        Task<List<Product>> PaginateProductsAsync(IQueryable<Product> products, int page, int take);
        Task<List<Product>> Filter(string title, int page, int take);
    }
}
