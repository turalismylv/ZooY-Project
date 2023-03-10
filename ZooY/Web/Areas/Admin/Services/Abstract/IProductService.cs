using Web.Areas.Admin.ViewModels.Product;

namespace Web.Areas.Admin.Services.Abstract
{
    public interface IProductService
    {
        Task<ProductIndexVM> GetAllAsync();
        Task<ProductCreateVM> GetCreateModelAsync();
        Task<bool> CreateAsync(ProductCreateVM model);
        Task<ProductUpdateVM> GetUpdateModelAsync(int id);
        Task<bool> UpdateAsync(ProductUpdateVM model);
        Task<bool> DeleteAsync(int id);
        Task<bool> AddTagsAsync(ProductAddTagsVM model);
        Task<ProductAddTagsVM> GetAddTagsModelAsync(int id);
        Task<ProductDetailsVM> GetDetailsModelAsync(int id);

    }
}
